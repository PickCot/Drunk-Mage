
using UnityEngine;

public class RiskyDrawButtonUI : MonoBehaviour
{
    public void OnClick()
    {
        if (DrunkennessSystem.Instance.IsDrunk)
            return;

        ActionSystem.Instance.Perform(new RiskyDrawGA(), () =>
        {
            if (DrunkennessSystem.Instance.ShouldEndTurn)
            {
                DelayedEffectSystem.Instance.ClearEffects();
                ActionSystem.Instance.Perform(new EnemyTurnGA());
            }
        });
    }
}
