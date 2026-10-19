using DG.Tweening;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSystem : Singleton<CardSystem>
{
    [SerializeField] private HandView handView;
    private readonly List<Card> drawPile = new();
    private readonly List<Card> discardPile = new();
    private readonly List<Card> hand = new();


    [SerializeField] private Transform drawPilePoint;

    [SerializeField] private Transform discardPilePoint;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<DrawCardsGA>(DrawCardsPerformer);
        ActionSystem.AttachPerformer<DiscardAllCardsGA>(DiscardAllCardsPerformer);
        ActionSystem.AttachPerformer<PlayCardGA>(PlayCardPerformer);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.SubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DrawCardsGA>();
        ActionSystem.DetachPerformer<DiscardAllCardsGA>();
        ActionSystem.DetachPerformer<PlayCardGA>();
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPreReaction, ReactionTiming.PRE);
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(EnemyTurnPostReaction, ReactionTiming.POST);
    }

    public void Setup(List<CardData> deckData)
    {
        foreach (var item in deckData)
        {
            Card card = new(item);
            drawPile.Add(card);
        }
    }

    private IEnumerator DrawCardsPerformer(DrawCardsGA drawCardsGA)
    {
        int actualAmount = Mathf.Min(drawCardsGA.Amount, drawPile.Count);
        int notDrawnAmount = drawCardsGA.Amount - actualAmount;

        for (int i = 0; i < actualAmount; i++)
        {
            yield return DrawCard();
        }

        if (notDrawnAmount > 0)
        {
            RefillDeck();

            for (int i = 0; i < notDrawnAmount; i++)
            {
                yield return DrawCard();
            }
        }
    }
    private IEnumerator DiscardAllCardsPerformer(DiscardAllCardsGA discardAllCardsGA)
    {
        foreach(var card in hand)
        {
            //discardPile.Add(card);
            CardView cardView = handView.RemoveCard(card);
            yield return DiscardCard(cardView);
        }
        hand.Clear();
    }

    //private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    //{
    //    hand.Remove(playCardGA.Card);
    //    CardView cardView = handView.RemoveCard(playCardGA.Card);
    //    yield return DiscardCard(cardView);

    //    SpendManaGA spendManaGA = new(playCardGA.Card.Mana);
    //    ActionSystem.Instance.AddReaction(spendManaGA);

    //    foreach (var effectWrapper in playCardGA.Card.OtherEffects)
    //    {
    //        PerformEffectGA performEffectGA = new(effectWrapper);
    //        ActionSystem.Instance.AddReaction(performEffectGA);
    //    }
    //}

    //private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    //{
    //    hand.Remove(playCardGA.Card);
    //    CardView cardView = handView.RemoveCard(playCardGA.Card);
    //    yield return DiscardCard(cardView);

    //    SpendManaGA spendManaGA = new(playCardGA.Card.Mana);
    //    ActionSystem.Instance.AddReaction(spendManaGA);

    //    if (playCardGA.Card.ManualTargetEffect != null)
    //    {
    //        if (playCardGA.ManualTarget == null)
    //        {
    //            Debug.LogError("Manual target is null");
    //            yield break;
    //        }
    //        PerformEffectGA performEffectGA = new(playCardGA.Card.ManualTargetEffect, new() { playCardGA.ManualTarget });
    //        ActionSystem.Instance.AddReaction(performEffectGA);
    //    }

    //    foreach (var effectWrapper in playCardGA.Card.OtherEffects)
    //    {
    //        List<CombatantView> targets = effectWrapper.TargetMode.GetTargets();
    //        PerformEffectGA performEffectGA = new(effectWrapper.Effect, targets);
    //        ActionSystem.Instance.AddReaction(performEffectGA);
    //    }
    //}


    private IEnumerator PlayCardPerformer(PlayCardGA playCardGA)
    {
        hand.Remove(playCardGA.Card);

        CardView cardView = handView.RemoveCard(playCardGA.Card);
        yield return DiscardCard(cardView);

        // Mana is spent immediately
        SpendManaGA spendManaGA = new(playCardGA.Card.Mana);
        ActionSystem.Instance.AddReaction(spendManaGA);

        // Manual target effect
        if (playCardGA.Card.ManualTargetEffect != null)
        {
            if (playCardGA.ManualTarget == null)
            {
                Debug.LogError("Manual target is null");
                yield break;
            }

            PerformEffectGA performEffectGA = new(
                playCardGA.Card.ManualTargetEffect,
                new List<CombatantView> { playCardGA.ManualTarget }
            );

            DelayedEffectSystem.Instance.AddEffect(performEffectGA);
        }

        // Automatic target effects
        foreach (var effectWrapper in playCardGA.Card.OtherEffects)
        {
            List<CombatantView> targets =
                effectWrapper.TargetMode.GetTargets();

            if (targets == null && effectWrapper.Effect is not DealHealthDamageEffect)
            {
                Debug.LogError(
                    $"Card '{playCardGA.Card.Title}' has no targets!"
                );
                continue;
            }

            PerformEffectGA performEffectGA = new(
                effectWrapper.Effect,
                targets
            );

            DelayedEffectSystem.Instance.AddEffect(performEffectGA);
        }
    }


    private IEnumerator DrawCard()
    {
        Card card = drawPile.Draw();
        hand.Add(card);
        CardView cardView = CardViewCreator.Instance.CreateCardView(card, drawPilePoint.position, drawPilePoint.rotation);
        yield return handView.AddCard(cardView);
    }

    private void RefillDeck()
    {
        drawPile.AddRange(discardPile);
        discardPile.Clear();
    }

    
    private IEnumerator DiscardCard(CardView cardView)
    {
        discardPile.Add(cardView.Card);
        cardView.transform.DOScale(Vector3.zero, 0.15f);
        Tween tween = cardView.transform.DOMove(discardPilePoint.position, 0.15f);
        yield return tween.WaitForCompletion();
        Destroy(cardView.gameObject );
    }


    //private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    //{
    //    DiscardAllCardsGA discardAllCardsGA = new();
    //    ActionSystem.Instance.AddReaction(discardAllCardsGA);
    //}
    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        DrawCardsGA drawCardsGA = new(5);
        ActionSystem.Instance.AddReaction(drawCardsGA);
    }

    private void EnemyTurnPreReaction(EnemyTurnGA enemyTurnGA)
    {
        // Execute queued card effects first
        List<GameAction> pendingEffects =
            DelayedEffectSystem.Instance.TakePendingEffects();

        foreach (GameAction effect in pendingEffects)
        {
            ActionSystem.Instance.AddReaction(effect);
        }

        // Discard remaining cards
        DiscardAllCardsGA discardAllCardsGA = new();
        ActionSystem.Instance.AddReaction(discardAllCardsGA);
    }
}