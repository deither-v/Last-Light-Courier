using UnityEngine;

public class CameraFader : MonoBehaviour
{
    [Header("Setup")]
    public Transform player;

    [Header("Fade Settings")]
    [Range(0f, 1f)] public float fadedAlpha = 0.3f;
    public float fadeSpeed = 5f;

    private Renderer currentObstacle;
    private Color originalColor;

    void Update()
    {
        Vector3 direction = player.position - transform.position;
        float distance = direction.magnitude;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, distance))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Renderer hitRenderer = hit.collider.GetComponent<Renderer>();

                if (currentObstacle != null && currentObstacle != hitRenderer)
                {
                    ResetObstacle();
                }

                if (currentObstacle == null)
                {
                    currentObstacle = hitRenderer;
                    originalColor = currentObstacle.material.color;

                    // NEW: Tell the rock it's time to become see-through
                    SetMaterialToFade(currentObstacle.material);
                }

                Color targetColor = new Color(originalColor.r, originalColor.g, originalColor.b, fadedAlpha);
                currentObstacle.material.color = Color.Lerp(currentObstacle.material.color, targetColor, Time.deltaTime * fadeSpeed);

                return;
            }
        }

        ResetObstacle();
    }

    void ResetObstacle()
    {
        if (currentObstacle != null)
        {
            currentObstacle.material.color = Color.Lerp(currentObstacle.material.color, originalColor, Time.deltaTime * fadeSpeed);

            // Once it's back to solid (Alpha is nearly 1)
            if (currentObstacle.material.color.a >= 0.95f)
            {
                currentObstacle.material.color = originalColor;

                // NEW: Tell the rock it's solid again so it fixes the "Z-Write" depth!
                SetMaterialToOpaque(currentObstacle.material);

                currentObstacle = null;
            }
        }
    }

    // --- THE "SHAPESHIFT" MATH ---
    // These functions manually talk to the Unity Standard Shader

    void SetMaterialToFade(Material mat)
    {
        mat.SetFloat("_Mode", 2); // 2 is the index for "Fade" mode
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0); // Turn off depth writing for transparency
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000; // Move it to the "Transparent" draw layer
    }

    void SetMaterialToOpaque(Material mat)
    {
        mat.SetFloat("_Mode", 0); // 0 is the index for "Opaque" mode
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        mat.SetInt("_ZWrite", 1); // Turn depth writing BACK ON! (This fixes Lumi)
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.DisableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = -1; // Reset to default "Geometry" draw layer
    }
}