using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public Vector3 direction;
    private Rigidbody enemyRb;
    private GameManager gameManager;
    public AudioClip audioClip;

    private void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        enemyRb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // translate doesnt account for phyics only rb does...
        enemyRb.velocity = direction * speed;
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
        bool isPlayer = collision.gameObject.CompareTag("Player");

        if (isPlayer)
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
