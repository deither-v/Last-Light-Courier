using UnityEngine;
using UnityEngine.SceneManagement; // This line is required!

public class MenuController : MonoBehaviour
{
    public void PlayGame()
    {
        // Replace "Level 1" with the exact name of your scene file
        SceneManager.LoadScene("Level 1");
    }
}
