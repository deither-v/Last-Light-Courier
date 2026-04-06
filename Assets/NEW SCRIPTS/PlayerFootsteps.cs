using UnityEngine;

public class FootstepSync : MonoBehaviour
{
    public AudioSource footstepSource;
    public AudioClip footstepClip;

    // This function will be called by the Animation Event
    public void PlayFootstep()
    {
        // Use PlayOneShot so sounds can overlap slightly for a natural feel
        footstepSource.PlayOneShot(footstepClip);
    }
}