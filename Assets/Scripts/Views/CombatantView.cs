using DG.Tweening;
using System.Drawing;
using TMPro;
using UnityEngine;

public class CombatantView : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private TMP_Text repText;
    [SerializeField] private SpriteRenderer spriteRenderer;

    public int MaxWealth { get; private set; }
    public int CurrentWealth { get; private set; }
    public int MaxReputation { get; private set; }
    public int CurrentReputation { get; private set; }

    public int MaxHealth { get; private set; }
    public int CurrentHealth { get; private set; }


    protected void SetupBase(int wealth, int reputation, Sprite image)
    {
        MaxWealth = CurrentWealth = wealth;
        MaxReputation = 5;
        CurrentReputation = reputation;

        spriteRenderer.sprite = image;

        MaxHealth = 10;
        CurrentHealth = 10;

        UpdateWealthText();
        UpdateReputationText(0);
        UpdateHealthText();
    }

    private void UpdateWealthText()
    {
        healthText.text = "Wealth: " + CurrentWealth;
    }
    private void UpdateReputationText(int change)
    {
        if(change > 0)
        {
            repText.text = "Reputation: " + CurrentReputation /*+ "[ +" + change + "]"*/;
        }
        else
        {
            repText.text = "Reputation: " + CurrentReputation /*+ "[ --" + change + "]"*/;
        }

    }

    public void Damage(int damageAmount)
    {
        CurrentWealth -= damageAmount;
        if (CurrentWealth < 0)
        {
            CurrentWealth = 0;
        }

        transform.DOShakePosition(0.2f, 0.5f);
        UpdateWealthText();
    }

    public void AddWealth(int amount)
    {
        CurrentWealth += amount;
        UpdateWealthText();
    }

    public void AddReputation(int amount)
    {
        if (CurrentReputation + amount > MaxReputation)
        {
            CurrentReputation = MaxReputation;
        }
        else if (CurrentReputation + amount < 0)
        {
            CurrentReputation = 0;
        }
        else
        {
            CurrentReputation += amount;
        }
        UpdateReputationText(amount);

        if(amount < 0)
        {
            transform.DOShakePosition(0.2f, 0.5f);
        }
    }

    public void DealDamage(int amount)
    {
        CurrentHealth = Mathf.Max(
            0,
            CurrentHealth - amount
        );

        UpdateHealthText();
    }

    public void Heal(int amount)
    {
        CurrentHealth = Mathf.Min(
            MaxHealth,
            CurrentHealth + amount
        );

        UpdateHealthText();
    }

    public void SetSortingOrder(int order)
    {
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>(true))
        {
            sr.sortingLayerName = "Characters";
            sr.sortingOrder = order;
        }

        foreach (TMP_Text text in GetComponentsInChildren<TMP_Text>(true))
        {
            MeshRenderer mr = text.GetComponent<MeshRenderer>();

            if (mr != null)
            {
                mr.sortingLayerName = "Characters";
                mr.sortingOrder = order + 10;
            }
        }
    }

    private void UpdateHealthText()
    {
        healthText.text =
            $"HP: {CurrentHealth}/{MaxHealth}";
    }

    public void SetHealth(int health)
    {
        MaxHealth = health;
        CurrentHealth = health;

        UpdateHealthText();
    }
}
