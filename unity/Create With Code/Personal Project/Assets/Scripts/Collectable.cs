using UnityEngine;

public class Collectable : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    public GameManager gameManager;
    private Rigidbody collectableRb;

    // Start is called before the first frame update
    void Start()
    {
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
        if (transform.position.z > 41)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
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
