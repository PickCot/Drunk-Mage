
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ReduceDrunkennessEffect : Effect
{
    [SerializeField] private int amount = 3;

    public override bool RequiresTargets => false;
    public override bool IsImmediate => true;

    public override GameAction GetGameAction(
        List<CombatantView> targets)
    {
        return new ReduceDrunkennessGA(amount);
    }
}
