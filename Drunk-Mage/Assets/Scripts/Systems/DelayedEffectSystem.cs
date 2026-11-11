using System.Collections.Generic;
using UnityEngine;

public class DelayedEffectSystem : Singleton<DelayedEffectSystem>
{
    private readonly Queue<GameAction> pendingActions = new();

    public int PendingCount => pendingActions.Count;

    public void AddEffect(GameAction action)
    {
        if (action == null)
            return;

        pendingActions.Enqueue(action);

        Debug.Log($"Effect queued: {action.GetType().Name}");
    }

    public List<GameAction> TakePendingEffects()
    {
        List<GameAction> effects = new(pendingActions);
        pendingActions.Clear();

        return effects;
    }

    public void ClearEffects()
    {
        pendingActions.Clear();
    }
}
