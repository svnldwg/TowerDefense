using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static bool gameEnded = false;

    public static void EndGame()
    {
        if (gameEnded) {
            return;
        }
        
        Debug.Log("Game Over!");
        gameEnded = true;
    }
}
