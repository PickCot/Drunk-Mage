using TMPro;
using UnityEngine;

public class VictoryPointSystem : Singleton<VictoryPointSystem>
{
    [SerializeField] private TMP_Text victoryPointsText;
    private const int WIN_POINTS = 10;

    public int VictoryPoints { get; private set; }

    private void Start()
    {
        UpdateText();
    }

    public void AddVictoryPoints(int amount)
    {
        VictoryPoints += amount;
        UpdateText();

        if (VictoryPoints >= WIN_POINTS)
        {
            GameManager.Instance.Win();
        }
    }

    private void UpdateText()
    {
        victoryPointsText.text = "Current victory points: " + VictoryPoints + "/10";
    }

    private void OnEnable()
    {
        ActionSystem.AttachPerformer<GainVictoryPointsGA>(GainVictoryPointsPerformer);

        ActionSystem.SubscribeReaction<EnemyTurnGA>(
            GainPointsFromReputation,
            ReactionTiming.POST
        );
    }

    private void OnDisable()
    {
        ActionSystem.DetachPerformer<GainVictoryPointsGA>();

        ActionSystem.UnsubscribeReaction<EnemyTurnGA>(
            GainPointsFromReputation,
            ReactionTiming.POST
        );
    }

    private System.Collections.IEnumerator GainVictoryPointsPerformer(GainVictoryPointsGA gainVictoryPointsGA)
    {
        AddVictoryPoints(gainVictoryPointsGA.Amount);
        yield return null;
    }

    private void GainPointsFromReputation(EnemyTurnGA enemyTurnGA)
    {
        int points = 0;

        foreach (var enemy in EnemySystem.Instance.Enemies)
        {
            if (enemy.CurrentReputation >= 5 && (enemy.CurrentWealth >= 0))
            {
                points++;
            }
        }

        if (points > 0)
        {
            GainVictoryPointsGA gainVictoryPointsGA = new(points);
            ActionSystem.Instance.AddReaction(gainVictoryPointsGA);
        }
    }
}
