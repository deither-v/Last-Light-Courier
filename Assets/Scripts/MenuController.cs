using UnityEngine;
using UnityEngine.SceneManagement; 

public class MenuController : MonoBehaviour
{
    public void PlayGame()
    {
        // Replace "Level 1" with the exact name of your scene file
        SceneManager.LoadScene("Level 1");
    }

    // NEW FUNCTION
    public void QuitGame()
    {
        Debug.Log("Game is exiting..."); // This shows in the Console to prove it works
        Application.Quit(); // This closes the actual app after it is built
    }
    public GameObject mainMenuArea; // Drag your Main Menu buttons group here
    public GameObject optionsPanel; // Drag your Options Panel here

    // This opens Options and hides the Main Buttons
    public void OpenOptions()
    {
        mainMenuArea.SetActive(false);
        optionsPanel.SetActive(true);
    }

    // This goes back to the Main Buttons
    public void CloseOptions()
    {
        mainMenuArea.SetActive(true);
        optionsPanel.SetActive(false);
    }
}