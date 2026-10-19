using UnityEngine;

public class DealHealthDamageGA : GameAction
{
    public int Amount { get; }
    public Health Target { get; }

    public DealHealthDamageGA(int amount, Health target)
    {
        Amount = amount;
        Target = target;
    }
}
