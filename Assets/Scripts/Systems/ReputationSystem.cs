using System.Collections;
using UnityEngine;

public class ReputationSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<IncreaseReputationGA>(Perform);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<IncreaseReputationGA>();
    }

    private IEnumerator Perform(IncreaseReputationGA action)
    {
        foreach (var target in action.Targets)
        {
            if (target == null) continue;

            target.AddReputation(action.Amount);

            yield return new WaitForSeconds(0.15f);
        }
    }
}