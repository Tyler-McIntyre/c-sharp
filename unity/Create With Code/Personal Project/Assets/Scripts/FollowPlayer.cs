using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public string playerTag = "Player"; // Tag to identify the player object
    private GameObject player; // Reference to the player object
    private Vector3 cameraOffset = new(0, 4, 8);

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
        }

        // Smoothly follow the player
        transform.position = Vector3.Lerp(transform.position, player.transform.position + cameraOffset, Time.deltaTime * 5);

        // Update camera position and look at the player
        transform.position = player.transform.position + cameraOffset;
        transform.LookAt(player.transform);
    }
}
