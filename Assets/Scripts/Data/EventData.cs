using SerializeReferenceEditor;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Event Card")]
public class EventCardData : ScriptableObject
{
    [field: SerializeField] public string Title { get; private set; }

    [field: SerializeField] public string Description { get; private set; }

    //[field: SerializeReference] public Effect Effect { get; private set; }

    public Sprite Image { get; private set; }

    [field: SerializeReference, SR]
    public Effect ManualTargetEffect { get; private set; } = null;
    [field: SerializeField] public List<AutoTargetEffect> OtherEffects { get; private set; }
}