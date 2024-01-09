using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public int damping = 25;
    private Rigidbody playerRb;
    private Camera mainCamera;
    public int playerShield = 0;

    public bool hasShield;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = mainCamera.transform.forward * speed * verticalInput;

        playerRb.AddForce(movement, ForceMode.Impulse);

        // Apply damping to control deceleration
        playerRb.velocity -= playerRb.velocity * damping * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Shield"))
        {
            Debug.Log("Powerup obtained");
            playerShield++;
            hasShield = true;
        }
        else if (collision.gameObject.CompareTag("Asteroid"))
        {
            playerShield--;
            if (playerShield == 0)
            {
                hasShield = false;
                Debug.Log("Shield Depleted!");
            }

            if (playerShield < 0)
            {
                Destroy(gameObject);
                Debug.Log("Game Over Man! Game Over!");
            }
        }
    }
}
