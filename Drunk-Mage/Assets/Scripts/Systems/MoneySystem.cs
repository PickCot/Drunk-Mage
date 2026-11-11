using TMPro;
using UnityEngine;

public class MoneySystem : Singleton<MoneySystem>
{
    [SerializeField] private TMP_Text moneyText;
    [SerializeField] private int startingMoney = 3;

    public int CurrentMoney { get; private set; }


private void Start()
    {
        CurrentMoney = startingMoney;
        UpdateMoneyText();
    }

    public bool HasEnoughMoney(int amount)
    {
        return CurrentMoney >= amount;
    }

    public void AddMoney(int amount)
    {
        CurrentMoney += amount;
        UpdateMoneyText();
    }

    public int SpendMoney(int amount)
    {
        int spentAmount = Mathf.Min(amount, CurrentMoney);

        CurrentMoney -= spentAmount;
        UpdateMoneyText();

        return spentAmount;
    }

    private void UpdateMoneyText()
    {
        moneyText.text = "Current money: " + CurrentMoney;
    }
}