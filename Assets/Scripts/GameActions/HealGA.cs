using UnityEngine;

public class HealGA : GameAction
{
    public int Amount { get; }
    public Health Target { get; }

    public HealGA(int amount, Health target)
    {
        Amount = amount;
        Target = target;
    }
}
