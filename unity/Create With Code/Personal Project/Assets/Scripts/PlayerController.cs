using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 20f;
    private int damping = 25;
    private Rigidbody playerRb;
    public int playerShield = 0;
    private GameManager gameManager;
    private AudioSource gameOverAudio;
    private AudioSource asteroidCollisionAudio;

    void Start()
    {
        GameObject audioClips = GameObject.Find("Audio Clips");
        gameOverAudio = audioClips.GetComponentsInChildren<AudioSource>()[0];
        asteroidCollisionAudio = audioClips.GetComponentsInChildren<AudioSource>()[1];
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
        newPosition.z = Mathf.Clamp(newPosition.z, gameManager.playerMinZBound, gameManager.playerMaxZBound);
        newPosition.x = Mathf.Clamp(newPosition.x, gameManager.playerMinXBound, gameManager.playerMaxXBound);

        // Apply the clamped position as the new position
        playerRb.MovePosition(newPosition);

        // Apply damping to control deceleration
        playerRb.velocity -= playerRb.velocity * damping * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shield"))
        {
            playerShield++;
        }

        if (collision.gameObject.CompareTag("Asteroid"))
        {
            // shield drained, game over
            if (playerShield == 0)
            {
                gameOverAudio.Play();

                Destroy(gameObject);

                // end game
                gameManager.gameOver = true;
                return;
            }
            asteroidCollisionAudio.Play();
            playerShield--;
        }

        if (collision.gameObject.CompareTag("Gold"))
        {
            gameManager.score++;
        }
    }
}
