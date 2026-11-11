using UnityEngine;
using System.Collections.Generic;

public class Card
{
    

    public string Title => data.name;
    public string Description => data.Description;
    public Sprite Image => data.Image;

    public int Mana { get; private set; }

    public int MoneyCost => data.MoneyCost;

    private readonly CardData data;

    public Effect ManualTargetEffect => data.ManualTargetEffect;
    public List<AutoTargetEffect> OtherEffects => data.OtherEffects;

    public Card(CardData cardData)
    {
        data = cardData;
        Mana = cardData.Mana;
    }

    public int GetMoneyCost(CombatantView manualTarget = null)
    {
        int cost = MoneyCost;

        if (ManualTargetEffect != null && manualTarget != null)
        {
            cost += ManualTargetEffect.GetMoneyCost(new() { manualTarget });
        }

        foreach (var wrapper in OtherEffects)
        {
            if (wrapper.Effect == null)
                continue;

            List<CombatantView> targets = new();

            if (wrapper.Effect.RequiresTargets)
            {
                if (wrapper.TargetMode == null)
                {
                    Debug.LogError(
                        $"Card '{Title}' requires a TargetMode!"
                    );
                    continue;
                }

                targets = wrapper.TargetMode.GetTargets();

                if (targets == null || targets.Count == 0)
                {
                    Debug.LogError(
                        $"Card '{Title}' has no valid targets!"
                    );
                    continue;
                }
            }

            cost += wrapper.Effect.GetMoneyCost(targets);
        }

        return cost;
    }
}
