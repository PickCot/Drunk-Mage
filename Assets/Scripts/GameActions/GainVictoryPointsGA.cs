using UnityEngine;

public class GainVictoryPointsGA : GameAction
{
    public int Amount { get; private set; }

    public GainVictoryPointsGA(int amount)
    {
        Amount = amount;
    }
}
