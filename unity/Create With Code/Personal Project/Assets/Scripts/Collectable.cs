using UnityEngine;

public class Collectable : MonoBehaviour
{
    public SpawnManager spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        spawnManager = FindAnyObjectByType<SpawnManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
            spawnManager.collectableCount--;
        }
    }
}
