using System.Collections.Generic;

public class StealMoneyGA : GameAction
{
    public int Amount { get; private set; }
    public List<CombatantView> Targets { get; private set; }

    public StealMoneyGA(int amount, List<CombatantView> targets)
    {
        Amount = amount;
        Targets = targets;
    }
}