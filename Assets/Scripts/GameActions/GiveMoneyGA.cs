using System.Collections.Generic;

public class GiveMoneyGA : GameAction
{
    public int Amount { get; private set; }
    public List<CombatantView> Targets { get; private set; }

    public GiveMoneyGA(int amount, List<CombatantView> targets)
    {
        Amount = amount;
        Targets = targets;
    }
}