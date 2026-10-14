using UnityEngine;

public class PlayerView : CombatantView
{
    [SerializeField] private int startingHealth = 30;
    [SerializeField] private Sprite portrait;

    private void Start()
    {
        SetupBase(0, 0, portrait);
        SetHealth(startingHealth);
    }
}