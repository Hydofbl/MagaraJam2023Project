using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public DungeonGenerationData DungeonGenerationData;

    private List<Vector2Int> _dungeonRooms;

    private void Start()
    {
        _dungeonRooms = DungeonCrawlerController.GenerateDungeon(DungeonGenerationData);
        SpawnRooms(_dungeonRooms);
    }

    private void SpawnRooms(IEnumerable<Vector2Int> rooms)
    {
        foreach(Vector2Int room in rooms)
        {
            RoomInfo roomInfo = new RoomInfo();
            roomInfo.X = room.x;
            roomInfo.Y = room.y;

            RoomController.Instance.AddRoom(roomInfo);
        }
    }
}
