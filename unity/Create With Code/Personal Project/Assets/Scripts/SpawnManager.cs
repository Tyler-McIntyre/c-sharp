using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] asteroidPrefab;
    public GameObject powerupPrefab;
    public GameObject collectablePrefab;
    public GameObject playerPrefab;

    private int enemySpawnRate = 2;
    private int collectableSpawnRate = 5;
    private int shieldSpawnRate = 7;

    private int minSpeed = 10;
    private int maxSpeed = 20;

    private float minXBound = 11;
    private float maxXBound = 85;
    private float zBound = 98;

    private int maxY = 3;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation);
        SpawnGameObjects();
    }

    // Update is called once per frame
    void Update()
    {
        // need logic for wave count
        if (gameManager.score > gameManager.waveCount * 2)
        {
            gameManager.waveCount++;
        }

        if (Input.GetKeyUp(KeyCode.Space) && !GameObject.FindGameObjectWithTag("Player"))
        {
            Debug.Log("Respawn Player");
            Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation);
        }
    }

    // TODO: Should be based on the time ellapsed
    // TODO: Add different types of asteroids and health
    IEnumerator SpawnAsteroids()
    {
        while (!gameManager.gameOver)
        {
            int asteroidsToSpawn = 1;
            // multiplies the # of asteroids to spawn by the wave count
            if (gameManager.timer % 20 == 0)
            {
                asteroidsToSpawn = (asteroidsToSpawn * gameManager.waveCount) + gameManager.waveCount;
            } 
            else
            {
                asteroidsToSpawn *= gameManager.waveCount;
            }

            for (int i = 0; i < asteroidsToSpawn; i++)
            {
                float randomXPos = Random.Range(minXBound, maxXBound);

                int randomPrefabIndex = Random.Range(0, 2);

                // Instantiate the asteroid
                GameObject newAsteroid = Instantiate(
                    asteroidPrefab[randomPrefabIndex],
                    new Vector3(randomXPos, maxY, -zBound),
                    transform.rotation
                    );

                Enemy asteroidScript = newAsteroid.GetComponent<Enemy>();
                float randomSpeed = Random.Range(minSpeed, maxSpeed);

                // move down the screen
                Vector3 direction = new(0, 0, randomSpeed * Time.deltaTime);

                asteroidScript.SetSpeed(randomSpeed);
                asteroidScript.SetDirection(direction);
            }

            yield return new WaitForSeconds(enemySpawnRate);
        }
    }

    IEnumerator SpawnCollectables()
    {
        while (!gameManager.gameOver)
        {
            int collectablesToSpawn = gameManager.waveCount < 3 ? 2 : gameManager.waveCount / 2;

            for (int i = 0; i < collectablesToSpawn; i++)
            {
                float randomXPos = Random.Range(minXBound, maxXBound);

                GameObject newCollectable = Instantiate(
                    collectablePrefab,
                    new Vector3(randomXPos, maxY, -zBound),
                    transform.rotation
                    );

                Collectable collectableScript = newCollectable.GetComponent<Collectable>();
                float randomSpeed = Random.Range(minSpeed, maxSpeed);

                // move down the screen
                Vector3 direction = new(0, 0, randomSpeed * Time.deltaTime);

                collectableScript.SetSpeed(randomSpeed);
                collectableScript.SetDirection(direction);
            }

            yield return new WaitForSeconds(collectableSpawnRate);
        }
    }

    IEnumerator SpawnPowerups()
    {
        while (!gameManager.gameOver)
        {
            int powerupCount = gameManager.waveCount > 5 ? 1 : Random.Range(1, 4);

            for (int i = 0; i < powerupCount; i++)
            {
                float randomXPos = Random.Range(minXBound, maxXBound);

                GameObject newPowerup = Instantiate(
                    powerupPrefab,
                    new Vector3(randomXPos, maxY, -zBound),
                    transform.rotation
                    );

                Powerup powerupScript = newPowerup.GetComponent<Powerup>();
                float randomSpeed = Random.Range(minSpeed, maxSpeed);

                // move down the screen
                Vector3 direction = new(0, 0, randomSpeed * Time.deltaTime);

                powerupScript.SetSpeed(randomSpeed);
                powerupScript.SetDirection(direction);
            }

            yield return new WaitForSeconds(shieldSpawnRate);
        }
    }

    void SpawnGameObjects()
    {
        StartCoroutine(SpawnPowerups());
        StartCoroutine(SpawnCollectables());
        StartCoroutine(SpawnAsteroids());
    }
}
