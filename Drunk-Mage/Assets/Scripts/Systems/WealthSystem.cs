using System.Collections;
using UnityEngine;

public class WealthSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<AddWealthGA>(Perform);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<AddWealthGA>();
    }

    private IEnumerator Perform(AddWealthGA action)
    {
        foreach (var target in action.Targets)
        {
            if (target == null) continue;

            target.AddWealth(action.Amount);

            yield return new WaitForSeconds(0.15f);
        }
    }
}