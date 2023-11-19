using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IngameUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text coinText;
    [SerializeField] private TMP_Text totalEnemyText;
    [SerializeField] private TMP_Text currentEnemyText;

    public static IngameUIManager Instance;

    private void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void SetTotalEnemy(int count)
    {
        totalEnemyText.text = count.ToString();
    }

    public void SetCurrentEnemy(int count)
    {
        currentEnemyText.text = count.ToString();
    }

    public void SetCoinAmount(int amount)
    {
        coinText.text = amount.ToString();
    }

    public void GoToNextScene()
    {
        ScenesManager.Instance.LoadScene("Level" + GameDataManager.Instance.GetLevel());
    }
}
