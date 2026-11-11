using System.Collections.Generic;
using UnityEngine;

public class NoTargetTM : TargetMode
{
    public override List<CombatantView> GetTargets()
    {
        return null;
    }
}
