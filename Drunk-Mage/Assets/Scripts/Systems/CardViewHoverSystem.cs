using UnityEngine;

public class CardViewHoverSystem : Singleton<CardViewHoverSystem>
{
    [SerializeField]
    private CardView cardViewHover;

    public void Show(Card card, Vector3 position)
    {
        if (cardViewHover == null) return;

        cardViewHover.gameObject.SetActive(true);
        cardViewHover.Setup(card);

        cardViewHover.SetSortingOrder(1000);

        cardViewHover.transform.position = position;
    }

    public void Hide()
    {
        if (cardViewHover == null) return;

        cardViewHover.gameObject.SetActive(false);
    }
}