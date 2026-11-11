
using UnityEngine;

public class HealthDamageTest : MonoBehaviour
{
    [SerializeField] private Health enemyHealth;

    [ContextMenu("Test Damage")]
    private void TestDamage()
    {
        DealHealthDamageGA action = new(3, enemyHealth);

        ActionSystem.Instance.Perform(action);
    }
}
