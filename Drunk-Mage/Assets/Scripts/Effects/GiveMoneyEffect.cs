using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GiveMoneyEffect : Effect
{
    [SerializeField] private int amount;

    public override GameAction GetGameAction(List<CombatantView> targets)
    {
        return new GiveMoneyGA(amount, targets);
    }

    public override int GetMoneyCost(List<CombatantView> targets)
    {
        return amount * targets.Count;
    }
}