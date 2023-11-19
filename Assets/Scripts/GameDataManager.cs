using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class GameDataManager : MonoBehaviour
{
    private int _level;
    private int _coin;

    public static GameDataManager Instance;

    private void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetLevel()
    {
        return _level;
    }

    public void SetLevel(int level)
    {
        _level = level;
    }

    public int GetCoin()
    {
        return _coin;
    }

    public void SetCoin(int coin)
    {
        _coin = coin;
    }

    public void AddCoin(int coin)
    {
        _coin += coin;
    }

    public void SaveNextLevel(int level)
    {
        PlayerPrefs.SetInt("Level", level);
    }
    
    public void SaveCoinAmount()
    {
        PlayerPrefs.SetInt("Coin", _coin);
    }

    public void LoadDatas()
    {
        if(PlayerPrefs.HasKey("Level"))
        {
            _level = PlayerPrefs.GetInt("Level");
        }
        else
        {
            SetLevel(1);
        }

        if(PlayerPrefs.HasKey("Coin"))
        {
            _coin = PlayerPrefs.GetInt("Coin");
        }
        else
        {
            SetCoin(0);
        }
    }

    private void OnApplicationQuit()
    {
        //PlayerPrefs.DeleteAll();
    }
}
