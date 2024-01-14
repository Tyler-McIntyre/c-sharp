using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    private bool inGameUiIsActive = false;

    public DifficultySetting difficultySetting = DifficultySetting.Undefined;

    public float timer = 0;
    public int waveCount = 1;
    public int score = 0;
    
    public GameObject playerPrefab;
    public GameObject startScreenUi;
    public GameObject inGameUi;
    public GameObject gameOverUi;

    // Update is called once per frame
    void Update()
    {
        bool shouldStartGame = difficultySetting != DifficultySetting.Undefined;
        if (shouldStartGame && !gameOver)
        {
            if (!GameObject.FindGameObjectWithTag("Player") && !gameOver)
            {
                Instantiate(playerPrefab, playerPrefab.transform.position, playerPrefab.transform.rotation);
            }

            UpdateInGameUI();

            TextMeshProUGUI scoreText = inGameUi.GetComponentsInChildren<TextMeshProUGUI>()[0];
            scoreText.text = $"Score: {score}";

            if (score > waveCount * 2)
            {
                waveCount++;
            }
        }

        if (gameOver)
        {
            ShowGameOverUI();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowGameOverUI()
    {
        gameOverUi.gameObject.SetActive(true);
    }

    private void UpdateInGameUI()
    {
        // enable in game UI
        if (!inGameUiIsActive)
        {
            ShowInGameUI();
            HideStartScreenUI();
        }

        UpdateShieldSlider();

        // increment the timer between the time this frame and the last frame was updated
        // Can be used to help calculate the best score (more points in less time is better)
        timer += Time.deltaTime;
    }

    private void ShowInGameUI()
    {
        inGameUi.gameObject.SetActive(true);
        inGameUiIsActive = true;
    }

    private void HideStartScreenUI()
    {
        startScreenUi.gameObject.SetActive(false);
    }

    private void UpdateShieldSlider()
    {
        int playerShield = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().playerShield;
        Slider shieldSlider = inGameUi.GetComponentInChildren<Slider>();
        shieldSlider.value = playerShield;
    }

    public void SetDifficulty(int difficulty)
    {
        difficultySetting = (DifficultySetting)difficulty;
        gameOver = false;
        // clear the screen
        FreshStart();

        Debug.Log(difficultySetting);
    }

    private void FreshStart()
    {
        // wipe screen
        GameObject[] asteroids = GameObject.FindGameObjectsWithTag("Asteroid");

        foreach (GameObject go in asteroids)
        {
            Destroy(go);
        }

        // wipe screen
        GameObject[] gold = GameObject.FindGameObjectsWithTag("Gold");

        foreach (GameObject go in gold)
        {
            Destroy(go);
        }

        GameObject[] shields = GameObject.FindGameObjectsWithTag("Shield");

        foreach (GameObject go in shields)
        {
            Destroy(go);
        }
    }
}
