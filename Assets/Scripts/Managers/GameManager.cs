using TMPro;
using UnityEngine;

// Define the different states of the game
public enum GameState
{
    Gameplay,
    Paused,
    GameOver,
    LevelEnd
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Store the current state of the game
    public GameState CurrentState;
    // Store the previous state of the game
    public GameState PreviousState;

    [Header("Screens")]
    public GameObject PauseScreen;
    public GameObject ResultScreen;

    // Flags to check if the game is over or the level end
    public bool IsGameOver = false;
    public bool IsLevelEnd = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            Debug.LogWarning("EXTRA " + this + " DELETED.");
        }
        else
        {
            Instance = this;
        }

        //DisableScreens();

        // If game stopped, continue game
        Time.timeScale = 1f;
        SetInGameDatas();
    }

    private void Update()
    {
        switch (CurrentState)
        {
            case GameState.Gameplay:
                // Gameplay State Codes
                CheckForPauseAndResume();
                break;

            case GameState.Paused:
                // Pause State Codes
                CheckForPauseAndResume();
                break;

            case GameState.LevelEnd:
                if (!IsLevelEnd)
                {
                    IsLevelEnd = true;
                    // Stop the game
                    Time.timeScale = 0f;
                    Debug.Log("Level Done");

                    // Save gained coins etc. could be shown
                    GameDataManager.Instance.SaveCoinAmount();
                    GameDataManager.Instance.SaveNextLevel(GameDataManager.Instance.GetLevel() + 1);

                    //DisplayResults();
                }
                break;
            case GameState.GameOver:
                // GameOver State Codes
                if (!IsGameOver)
                {
                    IsGameOver = true;
                    // Stop the game
                    Time.timeScale = 0f;
                    Debug.Log("Game is Over");

                    // Save gained coins etc. could be shown
                    GameDataManager.Instance.SaveCoinAmount();
                    GameDataManager.Instance.SaveNextLevel(GameDataManager.Instance.GetLevel());

                    //DisplayResults();
                }
                break;
            default:
                Debug.LogWarning("STATE DOES NOT EXIST.");
                break;
        }
    }

    public void ChangeState(GameState state)
    {
        CurrentState = state;
    }

    public void PauseGame()
    {
        if (CurrentState != GameState.Paused)
        {
            PreviousState = CurrentState;
            ChangeState(GameState.Paused);
            // Stop the game
            Time.timeScale = 0f;
            PauseScreen.SetActive(true);
            Debug.Log("Game is paused");
        }
    }

    public void ResumeGame()
    {
        if (CurrentState == GameState.Paused)
        {
            ChangeState(PreviousState);
            Time.timeScale = 1f;
            PauseScreen.SetActive(false);
            Debug.Log("Game is resumed");
        }
    }

    void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CurrentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void DisplayResults()
    {
        ResultScreen.SetActive(true);
    }

    void SetInGameDatas()
    {
        GameDataManager.Instance.LoadDatas();

        IngameUIManager.Instance.SetCoinAmount(GameDataManager.Instance.GetCoin());
    }
}
