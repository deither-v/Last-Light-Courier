using UnityEngine;

public class WorldWaypoint : MonoBehaviour
{
    private Transform playerTransform;

    void Start()
    {
        // Finds your player automatically
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerTransform = player.transform;
    }

    void Update()
    {
        if (playerTransform != null)
        {
            // Face the player
            transform.LookAt(playerTransform);
            
            // Hover up and down slightly
            float hover = Mathf.Sin(Time.time * 2f) * 0.2f;
            transform.localPosition += new Vector3(0, hover * Time.deltaTime, 0);
        }
    }
}