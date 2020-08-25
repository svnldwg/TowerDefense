using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject ui;

    public string menuSceneName = "MainMenu";

    public SceneFader sceneFader;

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
        sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void Menu()
    {
        Unfreeze();
        sceneFader.FadeTo(menuSceneName);
    }

    private void Unfreeze()
    {
        Time.timeScale = 1f;
    }
}
