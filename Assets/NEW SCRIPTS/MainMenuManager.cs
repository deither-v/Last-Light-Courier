using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading scenes

public class MainMenuManager : MonoBehaviour
{
    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject optionsPanel;

    // --- START BUTTON ---
    public void StartGame()
    {
        // Loads the next scene in your Build Settings. 
        // You can also use SceneManager.LoadScene("YourSceneName");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // --- OPTIONS BUTTON ---
    public void OpenOptions()
    {
        mainMenuPanel.SetActive(false); // Hide main menu
        optionsPanel.SetActive(true);   // Show options
    }

    // --- BACK BUTTON ---
    public void CloseOptions()
    {
        optionsPanel.SetActive(false);  // Hide options
        mainMenuPanel.SetActive(true);  // Show main menu
    }

    // --- QUIT BUTTON ---
    public void QuitGame()
    {
        Debug.Log("Game Quit!"); // This prints in the editor so you know it works
        Application.Quit();      // This closes the actual built game
    }
}