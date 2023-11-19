using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // Define the different states of the game
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver
    }

    // Store the current state of the game
    public GameState CurrentState;
    // Store the previous state of the game
    public GameState PreviousState;

    [Header("Screens")]
    public GameObject PauseScreen;
    public GameObject ResultScreen;

    [Header("Current Stat Displays")]
    public TMP_Text CurrentHealthDisplay;
    public TMP_Text CurrentCoinDisplay;

    // Flag to check if the game is over
    public bool IsGameOver = false;

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

            case GameState.GameOver:
                // GameOver State Codes
                if (!IsGameOver)
                {
                    IsGameOver = true;
                    // Stop the game
                    Time.timeScale = 0f;
                    Debug.Log("Game is Over");
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

    public void GameOver()
    {
        ChangeState(GameState.GameOver);
    }

    void DisplayResults()
    {
        ResultScreen.SetActive(true);
    }
}
