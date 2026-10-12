using UnityEngine;
using System.Collections.Generic;
using SerializeReferenceEditor;

[CreateAssetMenu(menuName = "Data/Cards")]
public class CardData : ScriptableObject
{
    [field: SerializeField]
    public string Description { get; private set; }

    [field: SerializeField]
    public int Mana { get; private set; }

    [field: SerializeField] 
    public int MoneyCost { get; private set; }

    [field: SerializeField]
    public Sprite Image { get; private set; }

    [field: SerializeReference, SR]
    public Effect ManualTargetEffect { get; private set; } = null;
    [field: SerializeField] public List<AutoTargetEffect> OtherEffects { get; private set; }
}
