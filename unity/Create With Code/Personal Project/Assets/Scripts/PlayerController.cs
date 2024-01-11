using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed = 20f;
    public int damping = 25;
    private Rigidbody playerRb;
    public int playerShield = 0;
    public bool hasShield;
    private GameManager gameManager;

    public float minZBound = -39f;  
    public float maxZBound = -1.5f;

    public float minXBound = 7.5f;
    public float maxXBound = 85f;

    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        // Calculate the new position after adding force
        Vector3 newPosition = playerRb.position + (-verticalInput * speed * Vector3.forward + horizontalInput * speed * Vector3.left) * Time.deltaTime;

        // Clamp the new position within the specified bounds
        newPosition.z = Mathf.Clamp(newPosition.z, minZBound, maxZBound);
        newPosition.x = Mathf.Clamp(newPosition.x, minXBound, maxXBound);

        // Apply the clamped position as the new position
        playerRb.MovePosition(newPosition);

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
        else if (collision.gameObject.CompareTag("Gold"))
        {
            gameManager.score++;
            Debug.Log(gameManager.score);
        }
    }

    // TODO: add projectiles
}
