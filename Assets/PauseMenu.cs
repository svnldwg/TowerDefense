using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;

    public void ShowPauseMenu()
    {
        ui.SetActive(true);

        Time.timeScale = 0f;
    }

    public void ContinueGame()
    {
        ui.SetActive(false);

        Unfreeze();
    }

    public void Retry()
    {
        Unfreeze();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Menu()
    {
        Debug.Log("Go to Menu");
    }

    private void Unfreeze()
    {
        Time.timeScale = 1f;
    }
}
