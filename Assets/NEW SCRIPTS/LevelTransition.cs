using UnityEngine;
using UnityEngine.SceneManagement; // Required for loading levels!

public class LevelTransition : MonoBehaviour
{
    [Header("Level to Load")]
    [Tooltip("Type the EXACT name of your next scene file here!")]
    public string nextLevelName = "Level 2";

    void OnTriggerEnter(Collider other)
    {
        // This checks if the thing that walked into the bubble is Lumi
        // by looking for her specific movement script!
        if (other.GetComponent<IsometricPlayerMovement>() != null)
        {
            Debug.Log("Level Complete! Loading: " + nextLevelName);
            SceneManager.LoadScene(nextLevelName);
        }
    }
}