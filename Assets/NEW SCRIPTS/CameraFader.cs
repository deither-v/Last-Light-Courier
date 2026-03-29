using UnityEngine;

public class CameraFader : MonoBehaviour
{
    [Header("Setup")]
    public Transform player; // Drag Lumi here

    [Header("Fade Settings")]
    [Range(0f, 1f)] public float fadedAlpha = 0.3f; // 0.3 means 30% visible
    public float fadeSpeed = 5f; // How fast it turns transparent

    private Renderer currentObstacle;
    private Color originalColor;

    void Update()
    {
        // Draw an invisible line between the Camera and Lumi
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        RaycastHit hit;

        // Shoot the laser!
        if (Physics.Raycast(transform.position, direction, out hit, distance))
        {
            // Did the laser hit a rock?
            if (hit.collider.CompareTag("Obstacle"))
            {
                Renderer hitRenderer = hit.collider.GetComponent<Renderer>();

                // If the camera sweeps across a NEW rock, reset the old one first
                if (currentObstacle != null && currentObstacle != hitRenderer)
                {
                    ResetObstacle();
                }

                // If this is a rock we haven't faded yet, save its original color!
                if (currentObstacle == null)
                {
                    currentObstacle = hitRenderer;
                    originalColor = currentObstacle.material.color;
                }

                // Smoothly fade the rock out
                Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, fadedAlpha);
                currentObstacle.material.color = Color.Lerp(currentObstacle.material.color, targetColor, Time.deltaTime * fadeSpeed);

                return; // Stop the script here so it doesn't instantly reset
            }
        }

        // If the laser hits nothing (or hits Lumi), make sure the rock goes back to solid!
        ResetObstacle();
    }

    // A mini-function that smoothly fades the rock back to normal
    void ResetObstacle()
    {
        if (currentObstacle != null)
        {
            currentObstacle.material.color = Color.Lerp(currentObstacle.material.color, originalColor, Time.deltaTime * fadeSpeed);

            // Once it's basically solid again, completely clear its memory
            if (currentObstacle.material.color.a >= originalColor.a - 0.05f)
            {
                currentObstacle.material.color = originalColor;
                currentObstacle = null;
            }
        }
    }
}