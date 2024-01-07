using System.Collections;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballPrefabs;
    public GameObject[] dogPrefabs;

    private readonly float spawnLimitXLeft = -33;
    private readonly float spawnLimitXRight = 11;
    private readonly float spawnPosY = 30;
    public float timeBetweenKeyDown = 0.05f;
    private float timeStamp;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(SpawnRandomBall));
    }

    private void Update()
    {
        // prevent rapid fire
        bool canSpawnDog = Time.time >= timeStamp;

        // instantiate dog on space bar pressed
        if (canSpawnDog && Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 spawnPos = new(gameObject.transform.position.x, 0, 0); // spawn from player position
            GameObject dogPrefab = dogPrefabs[0];
            Instantiate(dogPrefab, spawnPos, dogPrefab.transform.rotation);

            // set new timestamp to check if enough time has elappsed
            timeStamp = Time.time + timeBetweenKeyDown;
        }
    }

    // Spawn random ball at random x position at top of play area
    IEnumerator SpawnRandomBall()
    {
        while(true)
        {
            // Generate random ball index and random spawn position
            Vector3 spawnPos = new(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);

            var randomBallPrefabIndex = Random.Range(0, ballPrefabs.Length);

            // instantiate ball at random spawn location
            Instantiate(ballPrefabs[randomBallPrefabIndex], spawnPos, ballPrefabs[randomBallPrefabIndex].transform.rotation);

            // pass control back to unity after each object is returned
            yield return new WaitForSeconds(Random.Range(3.0f, 5.0f));
        }
    }
}
