using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
    [SerializeField] private EnemiesBoardView enemyBoardView;
    public List<EnemyView> Enemies => enemyBoardView.EnemyViews;

    [SerializeField] private Health currentEnemy;
    public Health CurrentEnemy => currentEnemy;

    [SerializeField] private Health playerHealth;
    [SerializeField] private int enemyAttackDamage = 3;

    void OnEnable()
    {
        ActionSystem.AttachPerformer<EnemyTurnGA>(EnemyTurnPerformer);
        ActionSystem.AttachPerformer<KillEnemyGA>(KillEnemyPerformer);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<EnemyTurnGA>();
        ActionSystem.DetachPerformer<KillEnemyGA>();
    }

    public void Setup(List<EnemyData> enemyDatas)
    {
        //foreach (var enemyData in enemyDatas)
        //{
        //    enemyBoardView.AddEnemy(enemyData);
        //}
    }

    private IEnumerator EnemyTurnPerformer(EnemyTurnGA enemyTurnGA)
    {
        
        if (CurrentEnemy == null || CurrentEnemy.CurrentHealth <= 0)
        {
            Debug.Log("Enemy is dead. Skipping enemy turn.");
            yield break;
        }

        Debug.Log("Enemy Turn");

        yield return new WaitForSeconds(2f);

        if (playerHealth == null)
        {
            Debug.LogError("EnemySystem: Player Health is not assigned!");
            yield break;
        }

        DealHealthDamageGA attack = new(
            enemyAttackDamage,
            playerHealth
        );

        ActionSystem.Instance.AddReaction(attack);

        Debug.Log("End Enemy Turn");
    }

    private IEnumerator KillEnemyPerformer(KillEnemyGA killEnemy)
    {
        yield return enemyBoardView.RemoveEnemy(killEnemy.EnemyView);
    }

    public void CheckLoseCondition()
    {
        if (enemyBoardView.EnemyViews.Count <= 3)
        {
            GameManager.Instance.Lose();
        }
    }
}