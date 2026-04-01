using UnityEngine;
using TMPro;

public class RunePuzzle : MonoBehaviour
{
    [Header("Puzzle Status")]
    public bool hasRune = false;

    [Header("Puzzle Objects")]
    public GameObject pickupRune;
    public GameObject dropoffRune;
    public GameObject carriedRune;
    public GameObject gate;

    [Header("UI Elements")]
    public TextMeshProUGUI interactionText;

    [Header("Gate Settings")]
    public float gateOpenSpeed = 2f;
    public float gateOpenHeight = 3.5f;

    [Header("Traps")] // NEW: The slot for your hidden fog
    public GameObject pedestalTrap;

    private bool nearPickup = false;
    private bool nearDropoff = false;
    private bool isGateOpening = false;
    private Vector3 gateTargetPosition;

    void Start()
    {
        if (dropoffRune != null) dropoffRune.SetActive(false);
        if (carriedRune != null) carriedRune.SetActive(false);
        if (gate != null) gateTargetPosition = gate.transform.position + new Vector3(0, gateOpenHeight, 0);

        if (interactionText != null) interactionText.gameObject.SetActive(false);
    }

    void Update()
    {
        // --- INTERACTION LOGIC (Press E) ---
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (nearPickup && !hasRune)
            {
                hasRune = true;
                pickupRune.SetActive(false);
                carriedRune.SetActive(true);

                // NEW: Spring the trap immediately when the rune is picked up!
                if (pedestalTrap != null)
                {
                    pedestalTrap.SetActive(true);
                }

                if (interactionText != null) interactionText.gameObject.SetActive(false);
            }
            else if (nearDropoff && hasRune)
            {
                hasRune = false;
                carriedRune.SetActive(false);
                dropoffRune.SetActive(true);
                isGateOpening = true;

                if (interactionText != null) interactionText.gameObject.SetActive(false);
            }
        }

        // --- GATE OPENING ANIMATION ---
        if (isGateOpening && gate != null)
        {
            gate.transform.position = Vector3.MoveTowards(gate.transform.position, gateTargetPosition, gateOpenSpeed * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PickupPedestal") && !hasRune)
        {
            nearPickup = true;
            if (interactionText != null)
            {
                interactionText.text = "Press E to get Light Rune";
                interactionText.gameObject.SetActive(true);
            }
        }

        if (other.CompareTag("DropoffPedestal") && hasRune)
        {
            nearDropoff = true;
            if (interactionText != null)
            {
                interactionText.text = "Press E to place Light Rune";
                interactionText.gameObject.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickupPedestal"))
        {
            nearPickup = false;
            if (interactionText != null) interactionText.gameObject.SetActive(false);
        }

        if (other.CompareTag("DropoffPedestal"))
        {
            nearDropoff = false;
            if (interactionText != null) interactionText.gameObject.SetActive(false);
        }
    }
}