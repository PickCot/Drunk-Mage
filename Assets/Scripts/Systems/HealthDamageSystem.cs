
using System.Collections;
using UnityEngine;

public class HealthDamageSystem : MonoBehaviour
{

    [SerializeField] private Health playerHealth;
    [SerializeField] private Health enemyHealth;


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

        if (action.Target.CurrentHealth > 0)
            yield break;

        if (action.Target == enemyHealth)
        {
            Debug.Log("Enemy defeated!");
            GameManager.Instance.Win();
        }
        else if (action.Target == playerHealth)
        {
            Debug.Log("Player defeated!");
            GameManager.Instance.Lose();
        }
    }

}
