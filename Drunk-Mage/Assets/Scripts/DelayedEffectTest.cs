using System.Collections.Generic;
using UnityEngine;

public class DelayedEffectTest : MonoBehaviour
{
    [ContextMenu("Test Queue")]
    private void TestQueue()
    {
        DelayedEffectSystem.Instance.AddEffect(
            new DealDamageGA(
                3,
                new List<CombatantView>()
            )
        );

        Debug.Log(
            "Pending effects: " +
            DelayedEffectSystem.Instance.PendingCount
        );
    }
}