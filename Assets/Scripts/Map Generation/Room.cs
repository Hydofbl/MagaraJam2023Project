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

public class Room : MonoBehaviour
{
    public int Width;
    public int Height;

    public int X;
    public int Y;

    public List<Door> DoorList = new List<Door>();

    [Header("Enemy Spawning")]
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private List<SpawningEnemyData> spawningEnemyDatas;

    public bool IsVisited;

    private void Start()
    {
        if(RoomController.Instance == null)
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
                    if (!GetLeft())
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.right:
                    if (!GetRight())
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.top:
                    if (!GetTop())
                    {
                        door.gameObject.SetActive(false);
                    }
                    break;
                case Door.DoorType.bottom:
                    if (!GetBottom())
                    {
                        door.gameObject.SetActive(false);
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
                    door.ConnectedRoom = RoomController.Instance.GetRoom(X - 1, Y);
                    break;
                case Door.DoorType.right:
                    door.ConnectedRoom = RoomController.Instance.GetRoom(X + 1, Y);
                    break;
                case Door.DoorType.top:
                    door.ConnectedRoom = RoomController.Instance.GetRoom(X, Y + 1);
                    break;
                case Door.DoorType.bottom:
                    door.ConnectedRoom = RoomController.Instance.GetRoom(X, Y - 1);
                    break;
            }
        });
    }

    public bool GetRight()
    {
        return RoomController.Instance.DoesRoomExist(X + 1, Y);
    }

    public bool GetLeft()
    {
        return RoomController.Instance.DoesRoomExist(X - 1, Y);
    }

    public bool GetTop()
    {
        return RoomController.Instance.DoesRoomExist(X, Y + 1);
    }

    public bool GetBottom()
    {
        return RoomController.Instance.DoesRoomExist(X, Y - 1);
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
        EnemySpawner.Instance.SpawnEnemies(spawningEnemyDatas, spawnPoints);
    }
}
