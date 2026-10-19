using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DealHealthDamageEffect : Effect
{
    [SerializeField] private int damageAmount = 3;

    public override GameAction GetGameAction(
        List<CombatantView> targets)
    {
        return new DealHealthDamageGA(
            damageAmount,
            EnemySystem.Instance.CurrentEnemy
        );
    }
}