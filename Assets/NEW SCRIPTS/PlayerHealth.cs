using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float damagePerSecond = 20f;

    [Header("Regeneration Settings")] // NEW: Settings for auto-healing!
    public float healthRegenPerSecond = 10f; // How fast health comes back
    public float regenDelay = 5f; // Wait 5 seconds before healing starts

    [Header("UI Elements")]
    public GameObject gameOverPanel;
    public Slider healthBar;
    public Image damageFlash;

    [Header("Flash Settings")]
    public Color flashColor = new Color(1f, 0f, 0f, 0.3f);
    public float flashFadeSpeed = 5f;

    private int darknessOverlaps = 0;
    private bool isDead = false;
    private float safeTimer = 0f; // NEW: Our internal stopwatch
    private IsometricPlayerMovement movementScript;

    void Start()
    {
        currentHealth = maxHealth;
        movementScript = GetComponent<IsometricPlayerMovement>();

        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }

        if (damageFlash != null) damageFlash.color = Color.clear;
    }

    void Update()
    {
        if (isDead) return;

        if (darknessOverlaps > 0)
        {
            // If she is in the dark, reset the safe stopwatch to zero!
            safeTimer = 0f;

            currentHealth -= damagePerSecond * Time.deltaTime;

            if (healthBar != null) healthBar.value = currentHealth;
            if (damageFlash != null) damageFlash.color = flashColor;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
        else
        {
            // Smoothly fade the red damage flash away
            if (damageFlash != null)
            {
                damageFlash.color = Color.Lerp(damageFlash.color, Color.clear, flashFadeSpeed * Time.deltaTime);
            }

            // --- NEW: REGENERATION LOGIC ---
            // Only try to heal if she is currently hurt
            if (currentHealth < maxHealth)
            {
                // Start counting up the stopwatch
                safeTimer += Time.deltaTime;

                // Has it been 5 seconds yet?
                if (safeTimer >= regenDelay)
                {
                    // Slowly heal her
                    currentHealth += healthRegenPerSecond * Time.deltaTime;

                    // Make sure her health doesn't accidentally go over 100!
                    if (currentHealth > maxHealth)
                    {
                        currentHealth = maxHealth;
                    }

                    // Update the visual health bar
                    if (healthBar != null) healthBar.value = currentHealth;
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Darkness"))
        {
            darknessOverlaps++;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Darkness"))
        {
            darknessOverlaps--;
        }
    }

    void Die()
    {
        isDead = true;
        currentHealth = 0;

        if (healthBar != null) healthBar.value = 0;
        if (damageFlash != null) damageFlash.color = Color.clear;
        if (movementScript != null) movementScript.enabled = false;
        if (gameOverPanel != null) gameOverPanel.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("Main menu");
    }
}