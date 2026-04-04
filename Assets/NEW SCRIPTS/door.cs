using UnityEngine;

public class GateController : MonoBehaviour
{
    private AudioSource audioSource;
    private bool hasPlayed = false;

    void Start()
    {
        // Get the Audio Source component attached to this Gate
        audioSource = GetComponent<AudioSource>();
    }

    // Call this function to open the gate and play the sound
    public void OpenGate()
    {
        if (audioSource != null && !hasPlayed)
        {
            audioSource.Play();
            hasPlayed = true; // Prevents the sound from overlapping/restarting
            
            Debug.Log("Gate sound playing!");
            
            // ADD YOUR ANIMATION CODE HERE
            // Example: GetComponent<Animator>().SetTrigger("Open");
        }
    }
}