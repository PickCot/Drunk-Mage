using UnityEngine;
using System.Collections;

public class DamageSystem : MonoBehaviour
{
    //[SerializeField] private GameObject damageVFX;

    void OnEnable()
    {
        ActionSystem.AttachPerformer<DealDamageGA>(DealDamagePerformer);
    }

    void OnDisable()
    {
        ActionSystem.DetachPerformer<DealDamageGA>();
    }

    private IEnumerator DealDamagePerformer(DealDamageGA dealDamageGA)
    {
        foreach (var target in dealDamageGA.Targets)
        {
            if (target == null)
            {
                Debug.LogError("Damage target is null");
                continue;
            }
            target.Damage(dealDamageGA.Amount);
            //Instantiate(damageVFX, target.transform.position, Quaternion.identity);

            yield return new WaitForSeconds(0.15f);

            if (target.CurrentWealth <= 0)
            {
                if (target is EnemyView enemyView)
                {
                    KillEnemyGA killEnemyGA = new(enemyView);
                    ActionSystem.Instance.AddReaction(killEnemyGA);
                }
                else
                {
                    // Do some game over logic
                }
            }
        }
    }

}
