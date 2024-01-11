using UnityEngine;

public class SpacePanels : MonoBehaviour
{
    private GameManager gameManager;
    private float speed = 1;
    private float startZ = -200;
    private float targetZ = 100;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.gameOver)
        {
            MoveSpacePanels();
        }
    }

    private void MoveSpacePanels()
    {
        // Move the panels smoothly from start to target position
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, targetZ + 4.5f), step);

        // Check if the panels have reached the target position
        if (transform.position.z > targetZ)
        {
            // Reset the panels to the start position
            transform.position = new Vector3(transform.position.x, transform.position.y, startZ);
        }
    }
}
