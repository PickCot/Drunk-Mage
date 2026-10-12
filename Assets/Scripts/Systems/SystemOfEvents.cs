using System.Collections.Generic;
using UnityEngine;

public class SystemOfEvents : MonoBehaviour
{
    [SerializeField] private List<EventCardData> eventCards;

    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<EnemyTurnGA>(ApplyRandomEvent, ReactionTiming.POST);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(ApplyRandomEvent, ReactionTiming.POST);
    }

    //private void ApplyRandomEvent(EnemyTurnGA enemyTurnGA)
    //{
    //    if (eventCards.Count == 0) return;

    //    EventCardData eventCard = eventCards[Random.Range(0, eventCards.Count)];

    //    Debug.Log("Event: " + eventCard.Title);

    //    foreach (var effectWrapper in eventCard.OtherEffects)
    //    {
    //        List<CombatantView> targets = effectWrapper.TargetMode.GetTargets();

    //        PerformEffectGA performEffectGA = new(
    //            effectWrapper.Effect,
    //            targets
    //        );

    //        ActionSystem.Instance.AddReaction(performEffectGA);
    //    }
    //}
    private void ApplyRandomEvent(EnemyTurnGA enemyTurnGA)
    {
        if (eventCards.Count == 0) return;

        EventCardData eventCard =
            eventCards[Random.Range(0, eventCards.Count)];

        Debug.Log("Event: " + eventCard.Title);

        if (eventCard.OtherEffects.Count == 0) return;

        AutoTargetEffect effectWrapper =
            eventCard.OtherEffects[
                Random.Range(0, eventCard.OtherEffects.Count)
            ];

        List<CombatantView> targets =
            effectWrapper.TargetMode.GetTargets();

        PerformEffectGA performEffectGA = new(
            effectWrapper.Effect,
            targets
        );

        ActionSystem.Instance.AddReaction(performEffectGA);
    }
}