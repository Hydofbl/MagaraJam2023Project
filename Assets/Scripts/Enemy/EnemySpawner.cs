using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance;

    private void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void SpawnEnemies(List<SpawningEnemyData> spawningEnemyDatas, List<Transform> spawnPoints)
    {
        foreach (var data in spawningEnemyDatas)
        {
            for (int i = 0; i < data.EnemyAmount; i++)
            {
                Instantiate(data.EnemyPref, spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);
            }
        }
    }
}
