using UnityEngine;

public class Collectable : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    private Rigidbody collectableRb;
    private GameManager gameManager;
    private AudioSource goldCollectedAudio;

    // Start is called before the first frame update
    void Start()
    {
        GameObject audioClips = GameObject.Find("Audio Clips");
        goldCollectedAudio = audioClips.GetComponentsInChildren<AudioSource>()[3];
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        collectableRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // translate doesnt account for phyics only rb does...
        collectableRb.velocity = direction * speed;
        // Set the y position to 1
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
            goldCollectedAudio.Play();
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
