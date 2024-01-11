using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    public int damping = 25;
    private Rigidbody playerRb;
    public int playerShield = 0;

    public bool hasShield;

    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        playerRb.AddForce(-verticalInput * speed * Vector3.forward, ForceMode.Impulse);
        playerRb.AddForce(horizontalInput * speed * Vector3.left, ForceMode.Impulse);

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
