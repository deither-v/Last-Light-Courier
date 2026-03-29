using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static bool isPaused = false;

    [Header("UI Reference")]
    public GameObject pauseMenuUI;

    void Update()
    {
        // Check if the player pressed Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);

        // This unfreezes the game!
        Time.timeScale = 1f;
        isPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);

        // This freezes EVERYTHING (physics, animations, etc.)
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void QuitToMenu()
    {
        // Make sure time is moving again before leaving, 
        // otherwise the Main Menu might be frozen too!
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main menu");
    }
}