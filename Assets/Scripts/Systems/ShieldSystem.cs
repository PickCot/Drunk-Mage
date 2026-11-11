using System.Collections;
using UnityEngine;

public class ShieldSystem : Singleton<ShieldSystem>
{
    [SerializeField] private Shield playerShield;

    public Shield PlayerShield => playerShield;

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<AddShieldGA>(
            AddShieldPerformer
        );
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<AddShieldGA>();
    }

    private IEnumerator AddShieldPerformer(AddShieldGA action)
    {
        if (action.Target != null)
        {
            action.Target.AddShield(action.Amount);
        }

        yield return null;
    }
}
