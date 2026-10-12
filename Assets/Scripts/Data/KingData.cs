using UnityEngine;

[CreateAssetMenu(menuName = "Data/King")]
public class KingData : ScriptableObject
{
    [field: SerializeField] public Sprite Image { get; private set; }
    [field: SerializeField] public int Wealth { get; private set; }
    [field: SerializeField] public int Reputation { get; private set; }
}