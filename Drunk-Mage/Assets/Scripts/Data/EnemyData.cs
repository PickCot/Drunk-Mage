using UnityEngine;

[CreateAssetMenu(menuName = "Data/Enemy")]
public class EnemyData : ScriptableObject
{
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public int Wealth { get; private set; }
    [field: SerializeField] public int PositiveReputationAmount { get; private set; }
}