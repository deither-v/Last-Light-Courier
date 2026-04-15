using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialFinish : MonoBehaviour
{
    [Header("UI Reference")]
    public GameObject endPanel;

    private void OnTriggerEnter(Collider other)
    {
        // Only trigger if Lumi (tagged Player) enters
        if (other.CompareTag("Player"))
        {
            if (endPanel != null)
            {
                endPanel.SetActive(true);

                // Pause the game world
                Time.timeScale = 0f;

                // Release the mouse so you can click the button
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // IMPORTANT: The menu won't work if time is still 0!
        SceneManager.LoadScene("Main menu");
    }
}