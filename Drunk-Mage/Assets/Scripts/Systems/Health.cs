using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 30;

    public int MaxHealth => maxHealth;
    public int CurrentHealth { get; private set; }

    public event Action<int, int> OnHealthChanged;
    public event Action OnDeath;

    private void Awake()
    {
        CurrentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        if (amount <= 0 || CurrentHealth <= 0)
            return;

        CurrentHealth = Mathf.Max(
            0,
            CurrentHealth - amount
        );

        OnHealthChanged?.Invoke(
            CurrentHealth,
            MaxHealth
        );

        if (CurrentHealth == 0)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || CurrentHealth <= 0)
            return;

        CurrentHealth = Mathf.Min(
            MaxHealth,
            CurrentHealth + amount
        );

        OnHealthChanged?.Invoke(
            CurrentHealth,
            MaxHealth
        );
    }
}
