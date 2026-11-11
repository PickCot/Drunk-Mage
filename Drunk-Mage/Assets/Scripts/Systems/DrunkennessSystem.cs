using System.Collections;
using System;
using UnityEngine;

public class DrunkennessSystem : Singleton<DrunkennessSystem>
{
    [SerializeField] private int maxDrunkenness = 2;

    public int CurrentDrunkenness { get; private set; }
    public int MaxDrunkenness => maxDrunkenness;

    public bool IsDrunk =>
        CurrentDrunkenness >= MaxDrunkenness;

    public event Action<int, int> OnDrunkennessChanged;
    public bool ShouldEndTurn { get; private set; }

    public void AddDrunkenness(int amount)
    {
        if (amount <= 0 || IsDrunk)
            return;

        CurrentDrunkenness = Mathf.Min(
            CurrentDrunkenness + amount,
            MaxDrunkenness
        );

        OnDrunkennessChanged?.Invoke(
            CurrentDrunkenness,
            MaxDrunkenness
        );

        if (IsDrunk)
        {
            ShouldEndTurn = true;
        }

        Debug.Log($"Drunkenness: {CurrentDrunkenness}/{MaxDrunkenness}");
    }

    public void ResetDrunkenness()
    {
        CurrentDrunkenness = 0;

        OnDrunkennessChanged?.Invoke(
            CurrentDrunkenness,
            MaxDrunkenness
        );
        ShouldEndTurn = false;
    }

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<AddDrunkennessGA>(
            AddDrunkennessPerformer
        );

        ActionSystem.AttachPerformer<ResetDrunkennessGA>(
            ResetDrunkennessPerformer
        );

        ActionSystem.SubscribeReaction<EnemyTurnGA>(
            EnemyTurnPostReaction,
            ReactionTiming.POST
        );
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<AddDrunkennessGA>();
        ActionSystem.DetachPerformer<ResetDrunkennessGA>();

        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(
            EnemyTurnPostReaction,
            ReactionTiming.POST
        );
    }

    private IEnumerator AddDrunkennessPerformer(
        AddDrunkennessGA action)
    {
        AddDrunkenness(action.Amount);
        yield return null;
    }


    private IEnumerator ResetDrunkennessPerformer(
        ResetDrunkennessGA action)
    {
        ResetDrunkenness();
        yield return null;
    }

    private void EnemyTurnPostReaction(EnemyTurnGA enemyTurnGA)
    {
        ActionSystem.Instance.AddReaction(
            new ResetDrunkennessGA()
        );
    }



}
