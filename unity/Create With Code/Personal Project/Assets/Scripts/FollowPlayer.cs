using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public string playerTag = "Player"; // Tag to identify the player object
    private GameObject player; // Reference to the player object
    private Vector3 cameraOffset = new Vector3(0, 1, -3);
    public float rotationSpeed = 0.5f;

    void LateUpdate()
    {
        // Find the player object using the tag
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag(playerTag);

            // If player is still null, it might not exist in the scene yet
            if (player == null)
            {
                return;
            }

            // Set the initial camera position
            transform.position = player.transform.position + cameraOffset;
        }

        // Smoothly follow the player
        transform.position = Vector3.Lerp(transform.position, player.transform.position + cameraOffset, Time.deltaTime * 5);

        float horizontalInput = Input.GetAxis("Horizontal");

        // Rotate around the player
        if (horizontalInput != 0)
        {
            Quaternion turnAngle = Quaternion.AngleAxis(horizontalInput * rotationSpeed, Vector3.up);
            cameraOffset = turnAngle * cameraOffset;
            cameraOffset = cameraOffset.normalized * cameraOffset.magnitude; // Maintain the original distance
        }

        // Update camera position and look at the player
        transform.position = player.transform.position + cameraOffset;
        transform.LookAt(player.transform);
    }
}
