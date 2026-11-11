
using TMPro;
using UnityEngine;

public class ShieldView : MonoBehaviour
{
    [SerializeField] private Shield shield;
    [SerializeField] private TMP_Text shieldText;

    private void OnEnable()
    {
        if (shield != null)
            shield.OnShieldChanged += UpdateShield;
    }

    private void Start()
    {
        UpdateShield(shield.CurrentShield);
    }

    private void OnDisable()
    {
        if (shield != null)
            shield.OnShieldChanged -= UpdateShield;
    }

    private void UpdateShield(int amount)
    {
        shieldText.text = $"{amount}";
    }
}
