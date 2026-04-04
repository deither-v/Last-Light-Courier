using UnityEngine;

public class StoneTrigger : MonoBehaviour
{
    [Header("Audio Settings")]
    [Tooltip("Drag the Gate object (with the AudioSource) here")]
    public AudioSource gateAudio; 

    private bool hasPlayed = false;

    // This runs automatically when the stone touches the "Socket" zone
    private void OnTriggerEnter(Collider other)
    {
        // 1. Check if the object we hit is tagged "Socket"
        // 2. Check if we haven't already played the sound
        if (other.CompareTag("Socket") && !hasPlayed)
        {
            if (gateAudio != null)
            {
                // Play the sound assigned to the Gate's AudioSource
                gateAudio.Play();
                
                // Mark as played so it doesn't spam the sound
                hasPlayed = true; 
                
                Debug.Log("SUCCESS: Stone placed in Socket. Gate sound playing!");
            }
            else
            {
                Debug.LogWarning("WARNING: No Gate AudioSource assigned to the Stone script!");
            }
        }
    }
}