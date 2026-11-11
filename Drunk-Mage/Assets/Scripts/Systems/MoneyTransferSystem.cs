using System.Collections;
using UnityEngine;

public class MoneyTransferSystem : MonoBehaviour
{
    private void OnEnable()
    {
        ActionSystem.AttachPerformer<StealMoneyGA>(StealMoneyPerformer);
        ActionSystem.AttachPerformer<GiveMoneyGA>(GiveMoneyPerformer);
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<StealMoneyGA>();
        ActionSystem.DetachPerformer<GiveMoneyGA>();
    }

    private IEnumerator StealMoneyPerformer(StealMoneyGA stealMoneyGA)
    {
        foreach (var target in stealMoneyGA.Targets)
        {
            if (target == null) continue;

            int stolenAmount = Mathf.Min(stealMoneyGA.Amount, target.CurrentWealth);

            target.Damage(stolenAmount);
            MoneySystem.Instance.AddMoney(stolenAmount);

            if (target.CurrentWealth <= 0 && target is EnemyView enemyView)
            {
                KillEnemyGA killEnemyGA = new(enemyView);
                ActionSystem.Instance.AddReaction(killEnemyGA);
            }

            yield return new WaitForSeconds(0.15f);
        }
    }

    private IEnumerator GiveMoneyPerformer(GiveMoneyGA giveMoneyGA)
    {
        if (!MoneySystem.Instance.HasEnoughMoney(giveMoneyGA.Amount))
        {
            yield break;
        }

        foreach (var target in giveMoneyGA.Targets)
        {
            if (target == null) continue;

            MoneySystem.Instance.SpendMoney(giveMoneyGA.Amount);

            target.AddWealth(giveMoneyGA.Amount);

            yield return new WaitForSeconds(0.15f);
        }
    }
}