using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider progressBar;
    public float fakeLoadTime = 3.0f; // Set this to 3 seconds

    public void StartGame()
    {
        StartCoroutine(LoadAsynchronously("Level 1"));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        loadingScreen.SetActive(true);
        
        // Reset the bar to 0
        progressBar.value = 0;

        // 1. Start loading the scene in the background
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        
        // This prevents the scene from actually switching until we say so
        operation.allowSceneActivation = false;

        float timer = 0;

        // 2. The "Fake" Loading Loop
        while (timer < fakeLoadTime)
        {
            timer += Time.deltaTime;
            
            // Move the progress bar based on time, not just the computer's speed
            progressBar.value = timer / fakeLoadTime;

            yield return null;
        }

        // 3. Finalize the load
        progressBar.value = 1f;
        yield return new WaitForSeconds(0.5f); // Brief pause at 100% for polish
        
        operation.allowSceneActivation = true;
    }
}