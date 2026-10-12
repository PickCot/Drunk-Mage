using NUnit.Framework;
using UnityEngine;
using UnityEngine.Splines;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using System.Linq;

public class HandView : MonoBehaviour
{
    [SerializeField] private SplineContainer splineContainer;

    private readonly List<CardView> cards = new();
    public IEnumerator AddCard(CardView cardView)
    {
        cards.Add(cardView);
        yield return UpdateCardPositions(0.15f);
    }

    public CardView RemoveCard(Card card)
    {
        CardView cardView = GetCardView(card);
        if (cardView == null) return null;
        cards.Remove(cardView);
        StartCoroutine(UpdateCardPositions(0.15f));
        return cardView;
    }
    public CardView GetCardView(Card card)
    {
        return cards.Where(cardView => cardView.Card == card).FirstOrDefault();

    }

    public IEnumerator UpdateCardPositions(float duration)
    {
        if (cards.Count == 0) yield break;

        float cardSpacing = 1f / 10f;
        float firstCardPosition = 0.5f - (cards.Count - 1) * cardSpacing / 2;

        Spline spline = splineContainer.Spline;

        for (int i = 0; i < cards.Count; i++)
        {
            cards[i].SetSortingOrder(i * 10);
            float p = firstCardPosition + i * cardSpacing;

            Vector3 localPosition = spline.EvaluatePosition(p);
            Vector3 worldPosition = splineContainer.transform.TransformPoint(localPosition);

            Vector3 forward = spline.EvaluateTangent(p);
            Vector3 up = spline.EvaluateUpVector(p);

            Quaternion rotation = Quaternion.LookRotation(
                -up,
                Vector3.Cross(-up, forward).normalized
            );

            cards[i].transform.DOMove(
                worldPosition + 0.01f * i * Vector3.back,
                duration
            );

            cards[i].transform.DORotate(rotation.eulerAngles, duration);
        }

        yield return new WaitForSeconds(duration);
    }

}
