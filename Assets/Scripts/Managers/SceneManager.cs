using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;
    void Awake()
    {
        Instance = this;
    }

    public enum Scene
    {
        MainMenuScene,
        Level1,
        Level2,
        Level3,
        Level4
    }

    public void LoadScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

    public void LoadScene(string sceneName) 
    {
        SceneManager.LoadScene(sceneName);
    }
    public void LoadNewGame()
    {
        SceneManager.LoadScene(Scene.Level1.ToString());
    }

    public void LoadWantedScene()
    {
        SceneManager.LoadScene(Scene.Level1.ToString());
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(Scene.MainMenuScene.ToString());
    }
}
