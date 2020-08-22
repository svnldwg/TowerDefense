using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private static int money;
    public static int lives;
    public int initialMoney = 400;
    public int initialLives = 20;

    public static int waves;

    private void Start() {
        money = initialMoney;
        lives = initialLives;
        waves = 0;
    }

    public static bool CanAfford(int amount)
    {
        return money >= amount;
    }

    public static int GetMoney()
    {
        return money;
    }

    public static void AddMoney(int _money)
    {
        money += _money;
    }

    public static void ReduceMoney(int _money)
    {
        money -= _money;
    }

    public static void ReduceLives()
    {
        lives--;
    }
}
