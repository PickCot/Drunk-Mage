using UnityEngine;
using System.Collections;

public class EffectSystems : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<PerformEffectGA>(PerformEffectPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<PerformEffectGA>();
    }

    private IEnumerator PerformEffectPerformer(PerformEffectGA performEffect)
    {

        if (performEffect.Effect == null)
        {
            Debug.LogError("PerformEffectGA has no Effect assigned");
            yield break;
        }
        GameAction effectAction = performEffect.Effect.GetGameAction(performEffect.Targets);
        ActionSystem.Instance.AddReaction(effectAction);
        yield return null;
    }
}
