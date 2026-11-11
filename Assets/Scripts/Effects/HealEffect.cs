using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HealEffect : Effect
{
    [SerializeField] private int healAmount = 3;

    public override bool RequiresTargets => false;

    public override GameAction GetGameAction(
        List<CombatantView> targets)
    {
        return new HealGA(
            healAmount,
            HealthSystem.Instance.PlayerHealth
        );
    }
}