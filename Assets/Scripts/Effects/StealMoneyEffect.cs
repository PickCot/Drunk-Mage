using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StealMoneyEffect : Effect
{
    [SerializeField] private int amount;

    public override GameAction GetGameAction(List<CombatantView> targets)
    {
        return new StealMoneyGA(amount, targets);
    }
}