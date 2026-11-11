using System.Collections.Generic;
using UnityEngine;

//public class AddReputationEffect : Effect
//{

//    [SerializeField] private int amount;

//    public override GameAction GetGameAction(List<CombatantView> targets)
//    {
//        //List<CombatantView> targets = new(EnemySystem.Instance.Enemies);
//        IncreaseReputationGA increaseReputationGA = new(amount, targets);
//        return increaseReputationGA;
//        //return new AddWealthGA(amount, targets);
//    }
//}
[System.Serializable]
public class AddReputationEffect : Effect
{
    [SerializeField] private int amount;

    public override GameAction GetGameAction(List<CombatantView> targets)
    {
        return new IncreaseReputationGA(amount, targets);
    }
}