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

    public int SpawnEnemies(List<SpawningEnemyData> spawningEnemyDatas, List<SpawnPoint> spawnPoints, Room room)
    {
        int count = 0;

        foreach (var data in spawningEnemyDatas)
        {
            for (int i = 0; i < data.EnemyAmount; i++)
            {
                SpawnPoint spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

                if(spawnPoint.IsUsed)
                {
                    i--;
                    continue;
                }

                // instantiate enemy and set it's room
                Instantiate(data.EnemyPref, spawnPoint.SpawnPointTransform.position, Quaternion.identity).GetComponent<Enemy>().Room = room;

                spawnPoint.IsUsed = true;
            }

            count += data.EnemyAmount;
        }

        return count;
    }
}
