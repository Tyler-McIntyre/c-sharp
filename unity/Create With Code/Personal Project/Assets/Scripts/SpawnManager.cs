using System.Collections;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SpawnManager : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public GameObject powerupPrefab;
    public GameObject collectablePrefab;
    public GameObject playerPrefab;

    private int enemySpawnRate = 2;
    private int collectableSpawnRate = 5;
    private int shieldSpawnRate = 7;

    private int minSpeed = 10;
    private int maxSpeed = 20;

    private float xBound = 40;
    private float zBound = 40;
    private int maxY = 1;

    public int waveCount = 1;

    public int score = 0;
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation);
        SpawnGameObjects();
    }

    // Update is called once per frame
    void Update()
    {
        // need logic for wave count
        if (score > waveCount * 2)
        {
            waveCount++;
        }

        if (Input.GetKeyUp(KeyCode.Space) && !GameObject.FindGameObjectWithTag("Player"))
        {
            Debug.Log("Respawn Player");
            Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation);
        }
    }

    // TODO: Should be based on the time ellapsed
    // TODO: Add different types of asteroids and health
    IEnumerator SpawnAsteroids(int asteroidsToSpawn = 1)
    {
        while (!gameOver)
        {
            // multiplies the # of asteroids to spawn by the wave count
            for (int i = 0; i < asteroidsToSpawn * waveCount * 2; i++)
            {
                float randomXPos = Random.Range(-xBound, xBound);

                // Instantiate the asteroid
                GameObject newAsteroid = Instantiate(
                    asteroidPrefab,
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
        while (!gameOver)
        {
            for (int i = 0; i < waveCount; i++)
            {
                float randomXPos = Random.Range(-xBound, xBound);

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
        while (!gameOver)
        {
            int powerupCount = waveCount > 5 ? 1 : Random.Range(1, 4);

            for (int i = 0; i < powerupCount; i++)
            {
                float randomXPos = Random.Range(-xBound, xBound);

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
