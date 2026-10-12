using System.Collections.Generic;
using UnityEngine;

public class MatchSetupSystem : MonoBehaviour
{
    //[SerializeField] private HeroData heroData;
    [SerializeField] private List<CardData> playerDeck;
    [SerializeField] private List<EnemyData> enemyDatas;

    private void Start()
    {
        //HeroSystem.Instance.Setup(heroData);
        EnemySystem.Instance.Setup(enemyDatas);
        //CardSystem.Instance.Setup(heroData.Deck);
        CardSystem.Instance.Setup(playerDeck);

        DrawCardsGA drawCardsGA = new(5);
        ActionSystem.Instance.Perform(drawCardsGA);
    }
}