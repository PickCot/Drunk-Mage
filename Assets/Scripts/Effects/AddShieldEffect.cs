using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AddShieldEffect : Effect
{
    [SerializeField] private int shieldAmount = 3;

    public override bool RequiresTargets => false;

    public override GameAction GetGameAction(
        List<CombatantView> targets)
    {
        return new AddShieldGA(
            shieldAmount,
            ShieldSystem.Instance.PlayerShield
        );
    }
}
