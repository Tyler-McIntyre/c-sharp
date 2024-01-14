using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public bool gameOver = true;
    private bool inGameUiIsActive = false;

    // bounds for enemies, powerups, and collectables
    public readonly float minXBound = 5;
    public readonly float maxXBound = 85;

    public readonly float maxZBound = 10;
    public readonly float minZBound = -60;

    // player bounds
    public float playerMinZBound = -39f;
    public float playerMaxZBound = -1.5f;

    public readonly float playerMinXBound = 3;
    public readonly float playerMaxXBound = 90;

    public DifficultySetting difficultySetting = DifficultySetting.Undefined;

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
