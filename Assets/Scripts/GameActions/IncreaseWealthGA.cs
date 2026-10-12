using System.Collections.Generic;

public class IncreaseWealthGA : GameAction
{
    public int Amount { get; private set; }
    public List<CombatantView> Targets { get; private set; }

    public IncreaseWealthGA(int amount, List<CombatantView> targets)
    {
        Amount = amount;
        Targets = targets;
    }
}