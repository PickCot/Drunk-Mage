using System.Collections;
using UnityEngine;

public class HealthSystem : Singleton<HealthSystem>
{
    [SerializeField] private Health playerHealth;

    public Health PlayerHealth => playerHealth;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<HealGA>(HealPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<HealGA>();
    }

    private IEnumerator HealPerformer(HealGA action)
    {
        if (action.Target != null)
        {
            action.Target.Heal(action.Amount);
        }

        yield return null;
    }
}