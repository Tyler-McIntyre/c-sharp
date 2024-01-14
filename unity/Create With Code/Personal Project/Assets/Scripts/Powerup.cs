using UnityEngine;

public class Powerup : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    private Rigidbody powerupRb;
    private GameManager gameManager;
    private AudioSource powerupAudio;

    private void Start()
    {
        GameObject audioClips = GameObject.Find("Audio Clips");
        powerupAudio = audioClips.GetComponentsInChildren<AudioSource>()[4];
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        powerupRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        powerupRb.velocity = direction * speed;
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);

        // destory if it leaves the area
        if (transform.position.z > gameManager.maxZBound)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            powerupAudio.Play();
            Destroy(gameObject);
        }
    }

    // used to set the initial magnitude
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    public void SetDirection(Vector3 newDirection)
    {
        direction = newDirection.normalized;
    }
}
