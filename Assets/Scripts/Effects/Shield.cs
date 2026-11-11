
using System;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class Shield : MonoBehaviour
{
    public int CurrentShield { get; private set; }

    public event Action<int> OnShieldChanged;

    public void AddShield(int amount)
    {
        if (amount <= 0) return;

        CurrentShield += amount;
        OnShieldChanged?.Invoke(CurrentShield);
    }

    public int AbsorbDamage(int damage)
    {
        if (damage <= 0) return 0;

        int absorbed = Mathf.Min(CurrentShield, damage);

        CurrentShield -= absorbed;
        OnShieldChanged?.Invoke(CurrentShield);

        return damage - absorbed;
    }

    public void ResetShield()
    {
        CurrentShield = 0;
        OnShieldChanged?.Invoke(CurrentShield);
    }

    [ContextMenu("Add 5 Shield")]
    private void TestAddShield()
    {
        AddShield(5);
        Debug.Log($"Current Shield: {CurrentShield}");
    }
}