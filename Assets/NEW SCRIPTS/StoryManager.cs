using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryManager : MonoBehaviour
{
    [Header("UI References")]
    public GameObject storyPanel;
    public TextMeshProUGUI storyDisplayText;
    public Button nextButton;
    public Button backButton;

    [Header("Story Content")]
    [TextArea(5, 10)]
    public string[] storyPages; // Type your story parts here in the Inspector!
    
    private int currentPage = 0;

    void Start()
    {
        UpdateStoryUI();
    }

    public void ShowStory()
    {
        storyPanel.SetActive(true);
        currentPage = 0;
        UpdateStoryUI();
    }

    public void NextPage()
    {
        if (currentPage < storyPages.Length - 1)
        {
            currentPage++;
            UpdateStoryUI();
        }
        else
        {
            // If they are at the end of the story, load Level 1!
            StartGame();
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdateStoryUI();
        }
    }

    void UpdateStoryUI()
    {
        storyDisplayText.text = storyPages[currentPage];

        // Hide the back button if we are on the first page
        backButton.gameObject.SetActive(currentPage > 0);

        // Change the text of the "Next" button to "Start" on the last page
        if (currentPage == storyPages.Length - 1)
        {
            nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Begin Journey";
        }
        else
        {
            nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "Next";
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level 1"); 
    }
}