using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class IsometricPlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float turnSpeed = 10f;

    [Header("Animation")]
    public Animator animator;

    private Rigidbody rb;
    private Vector3 inputVector;
    private Transform cameraTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        if (animator == null) animator = GetComponentInChildren<Animator>();

        // This automatically finds your Isometric Camera in the scene!
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
        else
        {
            cameraTransform = FindFirstObjectByType<Camera>().transform;
        }
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        // --- NEW CAMERA-RELATIVE LOGIC ---
        // 1. Find out which way the camera is facing
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // 2. Flatten the vectors so Lumi doesn't try to fly into the sky or dig into the dirt
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // 3. Combine your WASD keys with the camera's actual directions
        inputVector = (forward * vertical + right * horizontal).normalized;

        // --- ANIMATION LOGIC ---
        if (animator != null)
        {
            bool isWalking = inputVector.magnitude > 0.1f;
            animator.SetBool("IsMoving", isWalking);
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + inputVector * moveSpeed * Time.fixedDeltaTime);

        if (inputVector != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(inputVector);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Slams on the brakes so she doesn't slide down slopes
            rb.linearVelocity = Vector3.zero;
        }
    }
}