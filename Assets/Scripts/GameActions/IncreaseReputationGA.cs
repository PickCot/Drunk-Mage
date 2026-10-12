using System.Collections.Generic;

public class IncreaseReputationGA : GameAction
{
    public int Amount { get; private set; }
    public List<CombatantView> Targets { get; private set; }

    public IncreaseReputationGA(int amount, List<CombatantView> targets)
    {
        Amount = amount;
        Targets = targets;
    }
}