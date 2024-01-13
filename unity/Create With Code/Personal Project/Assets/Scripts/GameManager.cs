using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    public bool shouldStartGame = false;

    public DifficultySetting difficultySetting = DifficultySetting.Undefined;

    public float timer = 0;
    public int waveCount = 1;
    public int score = 0;

    public Button restartButton;
    public TextMeshProUGUI gameOverText;

    private bool inGameUiIsActive = false;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI shieldText;
    public GameObject shieldSlider;

    // Update is called once per frame
    void Update()
    {
        shouldStartGame = difficultySetting != DifficultySetting.Undefined;
        if (shouldStartGame && !gameOver)
        {
            if (!gameOver)
            {
                UpdateInGameUI();
            }

            if (gameOver)
            {
                ShowGameOverUI();
            }

            scoreText.text = $"Score: {score}";
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowGameOverUI()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
    }

    private void UpdateInGameUI()
    {
        // enable in game UI
        if (!inGameUiIsActive)
        {
            ShowInGameUI();
        }

        UpdateShieldSlider();

        // increment the timer between the time this frame and the last frame was updated
        // Can be used to help calculate the best score (more points in less time is better)
        timer += Time.deltaTime;
    }

    private void ShowInGameUI()
    {
        shieldSlider.gameObject.SetActive(true);
        shieldText.gameObject.SetActive(true);
        scoreText.gameObject.SetActive(true);
        inGameUiIsActive = true;
    }

    private void HideInGameUI()
    {
        shieldSlider.gameObject.SetActive(false);
        shieldText.gameObject.SetActive(false);
        scoreText.gameObject.SetActive(false);
        inGameUiIsActive = false;
    }

    private void UpdateShieldSlider() 
    {
        int playerShield = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerShield;
        shieldSlider.GetComponent<Slider>().value = playerShield;
    }
}
