using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

[Serializable]
public class RoomInfo
{
    public GameObject RoomPref;
    public int X;
    public int Y;
}

public class RoomController : MonoBehaviour
{
    public GameObject[] RoomPrefs;
    public List<Room> LoadedRooms = new List<Room>();

    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public Room CurrentRoom
    {
        get
        {
            return _currentRoom;
        }
        set
        {
            if (!_currentRoom)
            {
                _currentRoom = value;
                MoveCameraToRoom(_currentRoom);
            }
            else
            {
                if (_currentRoom != value)
                {
                    _currentRoom.IsVisited = true;

                    _currentRoom = value;
                    MoveCameraToRoom(_currentRoom);

                    if (!_currentRoom.IsVisited)
                    {
                        _currentRoom.SpawnEnemies();
                    }
                }
            }
        }
    }

    private Room _currentRoom;
    private RoomInfo _currentRoomData;
    private Queue<RoomInfo> _loadRoomQueue = new Queue<RoomInfo>();

    private bool _isLoadingRoom = false;
    private bool _isUnconnectedDoorsRemoved = false;

    public static RoomController Instance;

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

        if (Input.GetKeyDown(KeyCode.Space) && _currentRoom)
        {
            CurrentRoom.OpenDoors();
        }
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

    private void MoveCameraToRoom(Room room)
    {
        if (_currentRoom)
        {
            _currentRoom.CloseDoors();
        }

        virtualCamera.Follow = room.transform;
    }
}