
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrunkennessUI : MonoBehaviour
{
    [SerializeField] private Slider drunkennessSlider;
    [SerializeField] private TMP_Text drunkennessText;

    private DrunkennessSystem drunkennessSystem;

    private void Start()
    {
        drunkennessSystem = DrunkennessSystem.Instance;

        if (drunkennessSystem == null)
        {
            Debug.LogError("DrunkennessSystem not found!");
            return;
        }

        drunkennessSystem.OnDrunkennessChanged += UpdateUI;

        UpdateUI(
            drunkennessSystem.CurrentDrunkenness,
            drunkennessSystem.MaxDrunkenness
        );
    }

    private void OnDestroy()
    {
        if (drunkennessSystem != null)
            drunkennessSystem.OnDrunkennessChanged -= UpdateUI;
    }

    private void UpdateUI(int current, int max)
    {
        drunkennessSlider.maxValue = max;
        drunkennessSlider.value = current;

        drunkennessText.text = $"{current}/{max}";
    }
}
