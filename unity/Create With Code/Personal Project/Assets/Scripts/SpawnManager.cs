using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] asteroidPrefab;
    public GameObject powerupPrefab;
    public GameObject collectablePrefab;

    private int enemySpawnRate = 2;
    private int collectableSpawnRate = 5;
    private int shieldSpawnRate = 7;

    private int minSpeed = 10;
    private int maxSpeed = 20;

    private int maxY = 3;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        StartCoroutine(SpawnGameObjects());
    }

    IEnumerator SpawnAsteroids()
    {
        while (!gameManager.gameOver)
        {
            // Allows asteroids to be spawned on the start screen 
            DifficultySetting difficultySetting = 
                gameManager.difficultySetting == DifficultySetting.Undefined ? 
                DifficultySetting.Easy : gameManager.difficultySetting;

            int asteroidsToSpawn = (int)difficultySetting * gameManager.waveCount;

            for (int i = 0; i < asteroidsToSpawn; i++)
            {
                float randomXPos = Random.Range(gameManager.minXBound, gameManager.maxXBound);

                int randomPrefabIndex = Random.Range(0, 2);
                GameObject asteroidToSpawn = asteroidPrefab[randomPrefabIndex];

                // Instantiate the asteroid
                GameObject newAsteroid = Instantiate(
                    asteroidToSpawn,
                    new Vector3(randomXPos, maxY, gameManager.minZBound),
                    asteroidToSpawn.transform.rotation
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
                float randomXPos = Random.Range(gameManager.minXBound, gameManager.maxXBound);

                GameObject newCollectable = Instantiate(
                    collectablePrefab,
                    new Vector3(randomXPos, maxY, gameManager.minZBound),
                    collectablePrefab.transform.rotation
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
                float randomXPos = Random.Range(gameManager.minXBound, gameManager.maxXBound);

                GameObject newPowerup = Instantiate(
                    powerupPrefab,
                    new Vector3(randomXPos, maxY, gameManager.minZBound),
                    powerupPrefab.transform.rotation
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

    IEnumerator SpawnGameObjects()
    {
        if (!gameManager.gameOver)
        {
            StartCoroutine(SpawnPowerups());
            StartCoroutine(SpawnCollectables());
            StartCoroutine(SpawnAsteroids());
        }

        yield return null;
    }
}
