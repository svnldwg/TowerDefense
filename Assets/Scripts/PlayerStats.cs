using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int money;
    public static int lives;
    public int initialMoney = 400;
    public int initialLives = 20;

    public static void ReduceLives()
    {
        lives--;
        if (lives <= 0) {
            GameManager.EndGame();
        }
    }

    private void Start() {
        money = initialMoney;
        lives = initialLives;
    }
}
