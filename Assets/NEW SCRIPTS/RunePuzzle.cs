using UnityEngine;

public class RunePuzzle : MonoBehaviour
{
    [Header("Puzzle Status")]
    public bool hasRune = false;

    [Header("Puzzle Objects (Drag from Hierarchy)")]
    public GameObject pickupRune;       // The rune on the first pedestal
    public GameObject dropoffRune;      // The rune that appears on the empty pedestal
    public GameObject carriedRune;      // NEW: The rune attached to Lumi's back!
    public GameObject gate;             // The iron gate

    [Header("Gate Settings")]
    public float gateOpenSpeed = 2f;
    public float gateOpenHeight = 3.5f;

    private bool nearPickup = false;
    private bool nearDropoff = false;
    private bool isGateOpening = false;
    private Vector3 gateTargetPosition;

    void Start()
    {
        // Hide both the drop-off rune AND the backpack rune at the start
        if (dropoffRune != null) dropoffRune.SetActive(false);
        if (carriedRune != null) carriedRune.SetActive(false);

        if (gate != null) gateTargetPosition = gate.transform.position + new Vector3(0, gateOpenHeight, 0);
    }

    void Update()
    {
        // --- INTERACTION LOGIC (Press E) ---
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Pick up the rune
            if (nearPickup && !hasRune)
            {
                hasRune = true;
                pickupRune.SetActive(false);  // Hide pedestal rune
                carriedRune.SetActive(true);  // NEW: Show the rune on her back!
                Debug.Log("Picked up the Light Rune!");
            }
            // Drop off the rune
            else if (nearDropoff && hasRune)
            {
                hasRune = false;
                carriedRune.SetActive(false); // NEW: Hide the rune on her back!
                dropoffRune.SetActive(true);  // Show rune on the second pedestal
                isGateOpening = true;         // Open the gate
                Debug.Log("Placed the Light Rune! Gate opening!");
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
        if (other.CompareTag("PickupPedestal")) nearPickup = true;
        if (other.CompareTag("DropoffPedestal")) nearDropoff = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PickupPedestal")) nearPickup = false;
        if (other.CompareTag("DropoffPedestal")) nearDropoff = false;
    }
}