using UnityEngine;

public class WakeUp : MonoBehaviour
{
    [Header("Settings")]
    public GameObject pedestalArrow; // Drag the Arrow from your Hierarchy into this slot

    private void OnTriggerEnter(Collider other)
    {
        // Check if the thing hitting the rune is the Player
        if (other.CompareTag("Player"))
        {
            // 1. Make sure we actually assigned an arrow in the Inspector
            if (pedestalArrow != null)
            {
                pedestalArrow.SetActive(true);
                Debug.Log("Rune collected! Pedestal arrow is now visible.");
            }
            else
            {
                Debug.LogWarning("You forgot to drag the Pedestal Arrow into the script slot on the Rune!");
            }

            // 2. Disable the rune so you can't pick it up twice
            gameObject.SetActive(false);
        }
    }
}