using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class RoomInfo
{
    public GameObject RoomPref;
    public int X;
    public int Y;
}

public class DungeonController : MonoBehaviour
{
    public GameObject[] RoomPrefs;
    public List<Room> LoadedRooms = new List<Room>();

    [Header("Virtual Camera")]
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private CinemachineBrain brain;

    public Room CurrentRoom
    {
        get
        {
            return _currentRoom;
        }
        set
        {
            // First Room
            if (!_currentRoom)
            {
                _currentRoom = value;
                _currentRoom.Status = RoomStatus.cleared;
                StartCoroutine(MoveCameraToRoom(_currentRoom));
            }
            else
            {
                if (_currentRoom != value)
                {
                    _currentRoom.Status = RoomStatus.cleared;
                    _currentRoom.CloseDoors();

                    _currentRoom = value;
                    StartCoroutine(MoveCameraToRoom(_currentRoom));
                }
            }
        }
    }

    private Room _currentRoom;
    private RoomInfo _currentRoomData;
    private Queue<RoomInfo> _loadRoomQueue = new Queue<RoomInfo>();

    private bool _isLoadingRoom = false;
    private bool _isUnconnectedDoorsRemoved = false;

    private int _currentEnemyCount;

    public int TotalEnemyCount = 0;

    public int CurrentEnemyCount
    {
        get { return _currentEnemyCount; }
        set
        {
            _currentEnemyCount = value;

            if (_currentEnemyCount == 0)
            {
                // Something Something

                GameManager.Instance.ChangeState(GameState.LevelEnd);
            }
        }
    }

    public static DungeonController Instance;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        UpdateRoomQueue();
    }

    #region Map Generation
    void UpdateRoomQueue()
    {
        if (_isLoadingRoom)
        {
            return;
        }

        if (_loadRoomQueue.Count == 0)
        {
            if (!_isUnconnectedDoorsRemoved)
            {
                _isUnconnectedDoorsRemoved = true;
                RemoveUnconnectedDoors();
                ConnectDoors();
                CalculateEnemyCount();
                CurrentRoom = LoadedRooms.Find(room => room.X == 0 && room.Y == 0);
            }

            return;
        }

        _currentRoomData = _loadRoomQueue.Dequeue();
        _isLoadingRoom = true;

        LoadRoom(_currentRoomData);
    }

    public void AddRoom(RoomInfo roomInfo)
    {
        if (DoesRoomExist(roomInfo))
        {
            return;
        }

        RoomInfo newRoomData = new RoomInfo();
        newRoomData.RoomPref = RoomPrefs[UnityEngine.Random.Range(0, RoomPrefs.Length)];
        newRoomData.X = roomInfo.X;
        newRoomData.Y = roomInfo.Y;

        _loadRoomQueue.Enqueue(newRoomData);
    }

    void LoadRoom(RoomInfo roomInfo)
    {
        GameObject newRoomInfo = Instantiate(roomInfo.RoomPref);

        if (newRoomInfo.TryGetComponent<Room>(out Room room))
        {
            RegisterRoom(room);
        }
        else
        {
            Destroy(newRoomInfo);
            _isLoadingRoom = false;
        }
    }

    public void RegisterRoom(Room room)
    {
        room.transform.position = new Vector3(
            _currentRoomData.X * room.Width,
            _currentRoomData.Y * room.Height,
            0
        );

        room.X = _currentRoomData.X;
        room.Y = _currentRoomData.Y;
        room.transform.parent = transform;

        if(room.X == 0 && room.Y == 0)
        {
            room.IsStartingRoom = true;
        }

        _isLoadingRoom = false;

        LoadedRooms.Add(room);
    }

    public void RemoveUnconnectedDoors()
    {
        LoadedRooms.ForEach(room => room.RemoveUnconnectedDoors());
    }

    public void ConnectDoors()
    {
        LoadedRooms.ForEach(room => room.ConnectDoors());
    }
    #endregion

    public bool DoesRoomExist(RoomInfo info)
    {
        return _loadRoomQueue.Contains(info) || LoadedRooms.Exists(room => room.X == info.X && room.Y == info.Y);
    }

    public bool DoesRoomExist(int x, int y)
    {
        return _loadRoomQueue.Any(info => info.X == x && info.Y == y) || LoadedRooms.Find(room => room.X == x && room.Y == y);
    }

    public Room GetRoom(int x, int y)
    {
        return LoadedRooms.Find(room => room.X == x && room.Y == y);
    }

    IEnumerator MoveCameraToRoom(Room room)
    {
        virtualCamera.Follow = room.transform;

        // wait for camera's blend
        yield return new WaitForSeconds(brain.m_DefaultBlend.m_Time/4);   

        if (room.Status.Equals(RoomStatus.undiscovered))
        {
            room.SpawnEnemies();
        }
        else if (room.Status.Equals(RoomStatus.cleared))
        {
            room.OpenDoors();
        }
    }

    private void CalculateEnemyCount()
    {
        foreach(Room room in LoadedRooms)
        {
            if (room.IsStartingRoom)
                continue;

            TotalEnemyCount += room.GetTotalEnemyOnRoom();
            CurrentEnemyCount = TotalEnemyCount;

            IngameUIManager.Instance.SetTotalEnemy(TotalEnemyCount);
            IngameUIManager.Instance.SetCurrentEnemy(CurrentEnemyCount);
        }
    }
}