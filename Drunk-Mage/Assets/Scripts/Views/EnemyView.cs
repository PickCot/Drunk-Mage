using TMPro;
using UnityEngine;

public class EnemyView : CombatantView
{
    //[SerializeField] private TMP_Text reputationText;

    //public int PositiveReputationAmount { get; set; }

    public void Setup(EnemyData enemyData)
    {
        //PositiveReputationAmount = enemyData.PositiveReputationAmount;
        //UpdateAttackText();
        SetupBase(enemyData.Wealth, enemyData.PositiveReputationAmount, enemyData.Image);
    }

    //private void UpdateAttackText()
    //{
    //    reputationText.text = "Rep to Robin: " + PositiveReputationAmount;
    //}
}