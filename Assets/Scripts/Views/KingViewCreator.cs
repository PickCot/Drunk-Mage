using UnityEngine;

public class KingViewCreator : Singleton<KingViewCreator>
{
    [SerializeField] private KingView kingViewPrefab;

    public KingView CreateKingView(KingData kingData, Vector3 position, Quaternion rotation)
    {
        KingView kingView = Instantiate(kingViewPrefab, position, rotation);
        kingView.Setup(kingData);
        kingView.SetSortingOrder(50);
        return kingView;
    }
}