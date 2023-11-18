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
            SampleScene
        }

        public void LoadScene(Scene scene)
        {
            SceneManager.LoadScene(scene.ToString());
        }
        public void LoadNewGame()
        {
            SceneManager.LoadScene(Scene.SampleScene.ToString());
        }

        public void LoadWantedScene()
        {
            SceneManager.LoadScene(Scene.SampleScene.ToString());
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
