using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    private float maxY = 16f; 

    public float floatForce;
    private float gravityModifier = 1.5f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();

        // Apply a small upward force at the start of the game
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver)
        {
            // Calculate the new position after applying the force
            Vector3 newPosition = transform.position + Vector3.up * floatForce * Time.deltaTime;

            // Check if the new position is above maxY
            if (newPosition.y <= maxY)
            {
                // Apply the force to move the balloon up
                playerRb.AddForce(Vector3.up * floatForce);
            }
            else
            {
                // If it would go above maxY, set the position to maxY
                newPosition.y = maxY;
                transform.position = newPosition;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb or the ground, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb") || other.gameObject.CompareTag("Ground"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);
        }
    }
}
