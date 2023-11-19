using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreensManager : MonoBehaviour
{
    [SerializeField] GameObject firstScene;
    [SerializeField] GameObject secondScene;
    [SerializeField] GameObject thirdScene;
    [SerializeField] GameObject fourthScene;
    [SerializeField] GameObject intro;
    void Start()
    {
                GameDataManager.Instance.LoadDatas();
        if (GameDataManager.Instance.GetLevel() == 1)
        {
            firstScene.SetActive(true);
            secondScene.SetActive(false);
            thirdScene.SetActive(false);
            fourthScene.SetActive(false);

            if (PlayerPrefs.HasKey("isIntroPlayed"))
            {
                intro.SetActive(false);
            }
        }

        else if (GameDataManager.Instance.GetLevel() == 2)
        {
            firstScene.SetActive(false);
            secondScene.SetActive(true);
            thirdScene.SetActive(false);
            fourthScene.SetActive(false);
            intro.SetActive(false);  
        }

        else if (GameDataManager.Instance.GetLevel() == 3)
        {
            firstScene.SetActive(false);
            secondScene.SetActive(false);
            thirdScene.SetActive(true);
            fourthScene.SetActive(false);
            intro.SetActive(false);
        }

        else if (GameDataManager.Instance.GetLevel() >= 4)
        {
            firstScene.SetActive(false);
            secondScene.SetActive(false);
            thirdScene.SetActive(false);
            fourthScene.SetActive(true);
            intro.SetActive(false);
        }
    }


}
