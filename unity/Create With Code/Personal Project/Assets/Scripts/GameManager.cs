using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;
    public float timer = 0;
    public int waveCount = 1;
    public int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // increment the timer between the time this frame and the last frame was updated
        timer += Time.deltaTime;
    }
}
