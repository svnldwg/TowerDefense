using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameIsOver;

    public GameObject gameOverUI;

    private void Start() {
        gameIsOver = false;
    }

    private void Update()
    {
        if (gameIsOver) {
            return;
        }
        
        if (PlayerStats.lives <= 0) {
            EndGame();
        }
    }

    private void EndGame()
    {
        gameIsOver = true;

        gameOverUI.SetActive(true);
    }
}
