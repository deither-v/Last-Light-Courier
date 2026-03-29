using UnityEngine;
using UnityEngine.UI; // NEW: Required to talk to the Slider!
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float damagePerSecond = 20f;

    [Header("UI Elements")]
    public GameObject gameOverPanel;
    public Slider healthBar; // NEW: The slot for your health bar

    private bool isInDarkness = false;
    private bool isDead = false;
    private IsometricPlayerMovement movementScript;

    void Start()
    {
        currentHealth = maxHealth;
        movementScript = GetComponent<IsometricPlayerMovement>();

        if (gameOverPanel != null) gameOverPanel.SetActive(false);

        // NEW: Set the slider to full health when the level starts
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    void Update()
    {
        if (isDead) return;

        if (isInDarkness)
        {
            currentHealth -= damagePerSecond * Time.deltaTime;

            // NEW: Shrink the red bar as health drops
            if (healthBar != null) healthBar.value = currentHealth;

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Darkness")) isInDarkness = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Darkness")) isInDarkness = false;
    }

    void Die()
    {
        isDead = true;
        currentHealth = 0;

        // Make sure the health bar hits exactly zero so it doesn't look weird
        if (healthBar != null) healthBar.value = 0;

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