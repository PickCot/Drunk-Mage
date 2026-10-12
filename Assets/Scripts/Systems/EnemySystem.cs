using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySystem : Singleton<EnemySystem>
{
    [SerializeField] private EnemiesBoardView enemyBoardView;
    public List<EnemyView> Enemies => enemyBoardView.EnemyViews;

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
        foreach (var enemyData in enemyDatas)
        {
            enemyBoardView.AddEnemy(enemyData);
        }
    }

    private IEnumerator EnemyTurnPerformer(EnemyTurnGA enemyTurnGA)
    {
        Debug.Log("Enemy Turn");
        yield return new WaitForSeconds(2f);
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