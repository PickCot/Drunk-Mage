using UnityEngine;

public class DrawCardsGA : GameAction
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int Amount {  get; set; }
    public DrawCardsGA(int amount)
    {
        Amount = amount;
    }
}
