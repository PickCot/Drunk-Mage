using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class CardView : MonoBehaviour
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;
    [SerializeField] private TMP_Text mana;
    [SerializeField] private SpriteRenderer imageSR;
    [SerializeField] private GameObject wrapper;
    [SerializeField] private LayerMask dropLayer;

    public Card Card { get; private set; }
    private Vector3 dragStartPosition;
    private Quaternion dragStartRotation;


    public void Setup(Card card)
    {
        if (card == null) Debug.LogError("Card is null");
        if (title == null) Debug.LogError("Title is null");
        if (description == null) Debug.LogError("Description is null");
        if (mana == null) Debug.LogError("Mana is null");
        if (imageSR == null) Debug.LogError("ImageSR is null");

        Card = card;
        title.text = card.Title;
        description.text = card.Description;
        mana.text = card.Mana.ToString();
        //mana.color = Color.blue;
        imageSR.sprite = card.Image;

        SetSortingOrder(100);
    }

    //public void SetSortingOrder(int order)
    //{
    //    foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>(true))
    //    {
    //        sr.sortingLayerName = "Default";
    //        sr.sortingOrder = order;
    //    }

    //    foreach (TMP_Text text in GetComponentsInChildren<TMP_Text>(true))
    //    {
    //        MeshRenderer mr = text.GetComponent<MeshRenderer>();

    //        if (mr != null)
    //        {
    //            mr.sortingLayerName = "Default";
    //            mr.sortingOrder = order + 10;
    //        }
    //    }
    //}

    //public void SetSortingOrder(int order)
    //{
    //    foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>(true))
    //    {
    //        sr.sortingLayerName = "Cards";
    //        sr.sortingOrder = order;
    //    }

    //    foreach (TMP_Text text in GetComponentsInChildren<TMP_Text>(true))
    //    {
    //        MeshRenderer mr = text.GetComponent<MeshRenderer>();

    //        if (mr != null)
    //        {
    //            mr.sortingLayerName = "Cards";
    //            mr.sortingOrder = order + 10;
    //        }
    //    }
    //}

    public void SetSortingOrder(int order)
    {
        SortingGroup sortingGroup = GetComponent<SortingGroup>();

        if (sortingGroup != null)
        {
            sortingGroup.sortingLayerName = "Cards";
            sortingGroup.sortingOrder = order;
        }

        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>(true))
        {
            sr.sortingLayerName = "Cards";
            sr.sortingOrder = order;
        }

        foreach (TMP_Text text in GetComponentsInChildren<TMP_Text>(true))
        {
            MeshRenderer mr = text.GetComponent<MeshRenderer>();

            if (mr != null)
            {
                mr.sortingLayerName = "Cards";
                mr.sortingOrder = order + 10;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (!Interactions.Instance.PlayerCanHover()) return;

        wrapper.SetActive(false);

        Vector3 pos = new Vector3(
            transform.position.x,
            2f,
            0f
        );

        CardViewHoverSystem.Instance.Show(Card, pos);
    }

    private void OnMouseExit()
    {
        if (!Interactions.Instance.PlayerCanHover()) return;
        CardViewHoverSystem.Instance.Hide();
        wrapper.SetActive(true);
    }

    void OnMouseDown()
    {
        if (!Interactions.Instance.PlayerCanInteract()) return;
        SetSortingOrder(1000);
        dragStartPosition = transform.position;
        dragStartRotation = transform.rotation;
        if (Card.ManualTargetEffect != null)
        {
            ManualTargetSystem.Instance.StartTargeting(transform.position);
        }
        else
        {
            Interactions.Instance.PlayerIsDragging = true;
            wrapper.SetActive(true);
            CardViewHoverSystem.Instance.Hide();

            //dragStartPosition = transform.position;
            //dragStartRotation = transform.rotation;

            transform.rotation = Quaternion.Euler(0, 0, 0);
            transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
        }
    }

    private void OnMouseDrag()
    {
        if (!Interactions.Instance.PlayerCanInteract()) return;
        if (Card.ManualTargetEffect != null) return;
        transform.position = MouseUtil.GetMousePositionInWorldSpace(-1);
    }

    private void OnMouseUp()
    {
        if (!Interactions.Instance.PlayerCanInteract()) return;

        if (Card.ManualTargetEffect != null)
        {
            //EnemyView target = ManualTargetSystem.Instance.EndTargeting();

            //if (target != null && CanPlayCard() /*&& ManaSystem.Instance.HasEnoughMana(Card.Mana)*/)
            //{
            //    PlayCardGA playCardGA = new(Card, target);
            //    ActionSystem.Instance.Perform(playCardGA);
            //}
            CombatantView target = ManualTargetSystem.Instance.EndTargeting();

            if (target != null && CanPlayCard())
            {
                PlayCardGA playCardGA = new(Card, target);
                ActionSystem.Instance.Perform(playCardGA);
            }
            else
            {
                transform.position = dragStartPosition;
                transform.rotation = dragStartRotation;
            }
        }
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (/*ManaSystem.Instance.HasEnoughMana(Card.Mana)*/
                CanPlayCard()
                && Physics.Raycast(ray, out RaycastHit hit, 100f, dropLayer))
            {
                PlayCardGA playCardGA = new(Card);
                ActionSystem.Instance.Perform(playCardGA);
            }
            else
            {
                transform.position = dragStartPosition;
                transform.rotation = dragStartRotation;
            }
        }

        Interactions.Instance.PlayerIsDragging = false;
    }

    //private bool CanPlayCard()
    //{
    //    Debug.Log($"Mana: {Card.Mana}, MoneyCost: {Card.MoneyCost}, PlayerMoney: {MoneySystem.Instance.CurrentMoney}");
    //    return ManaSystem.Instance.HasEnoughMana(Card.Mana)
    //        && MoneySystem.Instance.HasEnoughMoney(Card.MoneyCost);
    //}
    private bool CanPlayCard(CombatantView manualTarget = null)
    {
        int moneyCost = Card.GetMoneyCost(manualTarget);

        return ManaSystem.Instance.HasEnoughMana(Card.Mana)
            && MoneySystem.Instance.HasEnoughMoney(moneyCost);
    }
}