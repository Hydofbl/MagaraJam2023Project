using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SpawningEnemyData
{
    public GameObject EnemyPref;
    public int EnemyAmount;
}

[Serializable]
public class SpawnPoint
{
    public Transform SpawnPointTransform;
    [HideInInspector]
    public bool IsUsed = false;
}

public enum RoomStatus
{
    undiscovered,
    cleared,
}

public class Room : MonoBehaviour
{
    [Header("Room Info")]
    public bool IsStartingRoom;
    public RoomStatus Status;

    public int Width;
    public int Height;

    public int X;
    public int Y;

    public List<Door> DoorList = new List<Door>();

    [Header("Enemy Spawning")]
    [SerializeField] private List<SpawnPoint> spawnPoints;
    [SerializeField] private List<SpawningEnemyData> spawningEnemyDatas;

    private int _spawnedEnemyCount;

    public int SpawnedEnemyCount
    {
        get 
        { 
            return _spawnedEnemyCount; 
        }
        set 
        {
            // Means setting enemyCount
            if (_spawnedEnemyCount == 0)
            {
                _spawnedEnemyCount = value;
            }
            // Means enemy dying
            else
            {
                _spawnedEnemyCount = value;

                if(_spawnedEnemyCount <= 0)
                {
                    OpenDoors();
                }
            }
        }
    }

    private void Start()
    {
        if(DungeonController.Instance == null)
        {
            return;
        }

        DoorList.AddRange(GetComponentsInChildren<Door>().Where(door => door.GetComponent<Door>() != null));
        RemoveUnconnectedDoors();
    }

    public void RemoveUnconnectedDoors()
    {
        DoorList.ForEach(door =>
        {
            switch (door.type)
            {
                case Door.DoorType.left:
                    if (!HasNextRoom(X-1, Y))
                    {
                        door.HasNextRoom = false;
                    }
                    break;
                case Door.DoorType.right:
                    if (!HasNextRoom(X + 1, Y))
                    {
                        door.HasNextRoom = false;
                    }
                    break;
                case Door.DoorType.top:
                    if (!HasNextRoom(X, Y+1))
                    {
                        door.HasNextRoom = false;
                    }
                    break;
                case Door.DoorType.bottom:
                    if (!HasNextRoom(X, Y-1))
                    {
                        door.HasNextRoom = false;
                    }
                    break;
            }
        }
        );
    }

    public void ConnectDoors()
    {
        DoorList.ForEach(door =>
        {
            switch (door.type)
            {
                case Door.DoorType.left:
                    door.ConnectedRoom = DungeonController.Instance.GetRoom(X - 1, Y);
                    break;
                case Door.DoorType.right:
                    door.ConnectedRoom = DungeonController.Instance.GetRoom(X + 1, Y);
                    break;
                case Door.DoorType.top:
                    door.ConnectedRoom = DungeonController.Instance.GetRoom(X, Y + 1);
                    break;
                case Door.DoorType.bottom:
                    door.ConnectedRoom = DungeonController.Instance.GetRoom(X, Y - 1);
                    break;
            }
        });
    }

    public bool HasNextRoom(int x, int y)
    {
        return DungeonController.Instance.DoesRoomExist(x, y);
    }

    public void OpenDoors()
    {
        DoorList.Where(door => door.gameObject.activeSelf).ToList()
            .ForEach(door => door.SetDoorStatus(true));
    }

    public void CloseDoors()
    {
        DoorList.Where(door => door.gameObject.activeSelf).ToList()
            .ForEach(door => door.SetDoorStatus(false));
    }

    public void SpawnEnemies()
    {
        _spawnedEnemyCount = EnemySpawner.Instance.SpawnEnemies(spawningEnemyDatas, spawnPoints, this);
    }

    public int GetTotalEnemyOnRoom()
    {
        int count = 0;

        spawningEnemyDatas.ForEach(data => count += data.EnemyAmount);

        return count;
    }
}
