using System.Collections.Generic;

[System.Serializable]
public abstract class Effect
{
    public virtual bool RequiresTargets => true;
    public virtual bool IsImmediate => false;

    public abstract GameAction GetGameAction(
        List<CombatantView> targets
    );

    public virtual int GetMoneyCost(
        List<CombatantView> targets
    )
    {
        return 0;
    }
}