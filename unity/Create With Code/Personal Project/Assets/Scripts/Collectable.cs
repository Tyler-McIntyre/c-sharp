using UnityEngine;

public class Collectable : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public SpawnManager spawnManager;
    private Rigidbody collectableRb;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = FindAnyObjectByType<SpawnManager>();
        collectableRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // translate doesnt account for phyics only rb does...
        collectableRb.velocity = direction * speed;
        // Set the y position to 1
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);

        // destory if it leaves the area
        if (transform.position.z > 41)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);

            // increment the score
            spawnManager.score++;
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
