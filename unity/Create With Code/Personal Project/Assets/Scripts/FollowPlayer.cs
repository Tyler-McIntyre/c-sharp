using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public string playerTag = "Player"; // Tag to identify the player object
    private GameObject player; // Reference to the player object
    private Vector3 cameraOffset = new(0, 20, 10);

    void LateUpdate()
    {

    }
}
