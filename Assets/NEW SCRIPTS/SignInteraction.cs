using UnityEngine;
using TMPro;

public class SignInteraction : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI interactionText;
    private RectTransform textRect;

    [Header("Sign Settings")]
    [TextArea] public string signMessage = "Beware of the darkness..";

    [Header("Color Settings")]
    public Color promptColor = Color.yellow;
    public Color messageColor = Color.red;

    [Header("Font Size Settings")]
    public float promptFontSize = 36f;
    public float messageFontSize = 45f;

    [Header("Position Settings")]
    public Vector2 promptPosition = new Vector2(0, 100);
    public Vector2 messagePosition = new Vector2(0, 0);

    private bool isPlayerNear = false;
    private bool isReading = false;

    void Start()
    {
        if (interactionText != null)
        {
            textRect = interactionText.GetComponent<RectTransform>();
            interactionText.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        // 1. Check for the "E" toggle
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            isReading = !isReading; // This flips the switch between true and false
        }

        // 2. LIVE PREVIEW LOGIC
        // If the player is near, we constantly update the UI with Inspector values
        if (isPlayerNear)
        {
            if (isReading)
            {
                ApplySettings(signMessage, messageColor, messageFontSize, messagePosition);
            }
            else
            {
                ApplySettings("Press E to read", promptColor, promptFontSize, promptPosition);
            }
        }
    }

    // A helper function to keep the code clean
    void ApplySettings(string text, Color color, float size, Vector2 pos)
    {
        if (interactionText != null)
        {
            interactionText.text = text;
            interactionText.color = color;
            interactionText.fontSize = size;
            if (textRect != null) textRect.anchoredPosition = pos;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            interactionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            isReading = false;
            interactionText.gameObject.SetActive(false);
        }
    }
}