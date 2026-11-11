using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private GameObject loseScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject gameplayRoot;

    public void Lose()
    {
        gameplayRoot.SetActive(false);
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Win()
    {
        gameplayRoot.SetActive(false);
        winScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}