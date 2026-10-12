using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingSystem : Singleton<KingSystem>
{
    [SerializeField] private KingData kingData;
    [SerializeField] private Transform kingSlot;

    public KingView KingView { get; private set; }

    private void OnEnable()
    {
        ActionSystem.SubscribeReaction<EnemyTurnGA>(AddKingTaxReaction, ReactionTiming.POST);
        ActionSystem.AttachPerformer<KingTaxGA>(KingTaxPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(AddKingTaxReaction, ReactionTiming.POST);
        ActionSystem.DetachPerformer<KingTaxGA>();
    }

    private void Start()
    {
        KingView = KingViewCreator.Instance.CreateKingView(
            kingData,
            kingSlot.position,
            kingSlot.rotation
        );
    }

    private void AddKingTaxReaction(EnemyTurnGA enemyTurnGA)
    {
        KingTaxGA kingTaxGA = new();
        ActionSystem.Instance.AddReaction(kingTaxGA);
    }

    private IEnumerator KingTaxPerformer(KingTaxGA kingTaxGA)
    {
        List<CombatantView> targets = new(EnemySystem.Instance.Enemies);

        foreach (var target in targets)
        {
            if (target == null) continue;

            int stolenAmount = Mathf.Min(1, target.CurrentWealth);

            target.Damage(stolenAmount);
            KingView.AddWealth(stolenAmount);

            if (target.CurrentWealth <= 0 && target is EnemyView enemyView)
            {
                KillEnemyGA killEnemyGA = new(enemyView);
                ActionSystem.Instance.AddReaction(killEnemyGA);
            }

            yield return new WaitForSeconds(0.15f);
        }
    }
}