using System.Collections.Generic;
using UnityEngine;

public class AddWealthEffect : Effect
{
    [SerializeField] private int amount;

    public override GameAction GetGameAction(List<CombatantView> targets)
    {
        //List<CombatantView> targets = new(EnemySystem.Instance.Enemies);
        AddWealthGA addWealthGA = new(amount, targets);
        return addWealthGA;
        //return new AddWealthGA(amount, targets);
    }
}