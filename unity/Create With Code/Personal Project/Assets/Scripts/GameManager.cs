using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameOver = false;
    public bool isActive = false;

    public float timer = 0;
    public int waveCount = 1;
    public int score = 0;

    public Button restartButton;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;

    // Start is called before the first frame update
    void Start()
    {
        restartButton = restartButton.GetComponent<Button>();
        gameOverText = gameOverText.GetComponent<TextMeshProUGUI>();
        scoreText = scoreText.GetComponent<TextMeshProUGUI>();
        scoreText.text = $"Score: {score}";
    }

    // Update is called once per frame
    void Update()
    {
        // increment the timer between the time this frame and the last frame was updated
        timer += Time.deltaTime;

        if (gameOver)
        {
            restartButton.gameObject.SetActive(true);
            gameOverText.gameObject.SetActive(true);
        }

        scoreText.text = $"Score: {score}";
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StartGame()
    {
        isActive = true;
    }
}
