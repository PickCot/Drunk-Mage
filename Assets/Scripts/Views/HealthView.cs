using TMPro;
using UnityEngine;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private TMP_Text healthText;

    private void OnEnable()
    {
        if (health == null || healthText == null)
            return;

        health.OnHealthChanged += UpdateHealth;
        //UpdateHealth(
        //    health.CurrentHealth,
        //    health.MaxHealth
        //);
    }
    private void Start()
    {
        if (health == null || healthText == null)
            return;

        UpdateHealth(health.CurrentHealth, health.MaxHealth);
    }

    private void OnDisable()
    {
        if (health != null)
            health.OnHealthChanged -= UpdateHealth;
    }

    private void UpdateHealth(int current, int max)
    {
        healthText.text = $"HP: {current}/{max}";
    }
}