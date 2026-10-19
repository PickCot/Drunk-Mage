
using System.Collections;
using UnityEngine;

public class HealthDamageSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<DealHealthDamageGA>(
            DealHealthDamagePerformer
        );
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<DealHealthDamageGA>();
    }

    private IEnumerator DealHealthDamagePerformer(
        DealHealthDamageGA action)
    {
        if (action.Target == null)
        {
            Debug.LogError("Health damage target is null!");
            yield break;
        }

        action.Target.TakeDamage(action.Amount);

        Debug.Log(
            $"Dealt {action.Amount} damage. " +
            $"Remaining HP: {action.Target.CurrentHealth}"
        );

        yield return new WaitForSeconds(0.15f);
    }
}
