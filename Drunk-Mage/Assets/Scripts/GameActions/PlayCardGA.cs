using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayCardGA : GameAction
{
    //public EnemyView ManualTarget { get; private set; }
    public CombatantView ManualTarget { get; private set; }
    public Card Card { get; set; }
    public PlayCardGA (Card card)
    {
        Card = card;
        ManualTarget = null;
    }
    //public PlayCardGA(Card card, EnemyView target)
    //{
    //    Card = card;
    //    ManualTarget = target;
    //}
    public PlayCardGA(Card card, CombatantView manualTarget)
    {
        Card = card;
        ManualTarget = manualTarget;
    }
}
