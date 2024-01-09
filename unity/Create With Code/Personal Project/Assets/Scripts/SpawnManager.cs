using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public GameObject powerupPrefab;
    public GameObject collectablePrefab;
    public GameObject playerPrefab;

    private float xBound = 22;
    private float zBound = 19;

    public int waveCount = 1;
    public int collectableCount = 0;

    private int maxY = 1;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(playerPrefab, new Vector3(0, 1, 0), playerPrefab.transform.rotation);
        SpawnAsteroids();
        SpawnCollectables();
    }

    // Update is called once per frame
    void Update()
    {
        if (collectableCount == 0)
        {
            waveCount++;
            SpawnPowerups();
            SpawnCollectables();
            SpawnAsteroids();
        }

        if (Input.GetKeyUp(KeyCode.Space) && !GameObject.FindGameObjectWithTag("Player"))
        {
            Debug.Log("Respawn Player");
            Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation);
        }
    }

    void SpawnAsteroids(int asteroidsToSpawn = 1)
    {
        // multiplies the # of asteroids to spawn by the wave count
        for (int i = 0; i < asteroidsToSpawn * waveCount / 2; i++)
        {
            float randomXPos = Random.Range(-xBound, xBound);
            float randomZPos = Random.Range(-zBound, zBound);

            // Instantiate the asteroid
            GameObject newAsteroid = Instantiate(asteroidPrefab, new Vector3(randomXPos, maxY, randomZPos), transform.rotation);

            Enemy asteroidScript = newAsteroid.GetComponent<Enemy>();
            float randomSpeed = Random.Range(1, 10);

            // This gives a random direction in 3D space
            Vector3 randomDirection = Random.insideUnitSphere.normalized;

            asteroidScript.SetSpeed(randomSpeed);
            asteroidScript.SetDirection(randomDirection);
        }
    }

    void SpawnCollectables()
    {
        for (int i = 0; i < waveCount / 2; i++)
        {
            float randomXPos = Random.Range(-xBound, xBound);
            float randomZPos = Random.Range(-zBound, zBound);
            Instantiate(collectablePrefab, new Vector3(randomXPos, maxY, randomZPos), transform.rotation);
            collectableCount++;
        }
    }

    void SpawnPowerups()
    {
        int powerupCount = waveCount > 5 ? 1 : Random.Range(1, 4);

        for (int i = 0; i < powerupCount; i++)
        {
            float randomXPos = Random.Range(-xBound, xBound);
            float randomZPos = Random.Range(-zBound, zBound);
            Instantiate(powerupPrefab, new Vector3(randomXPos, maxY, randomZPos), transform.rotation);
        }
    }
}
