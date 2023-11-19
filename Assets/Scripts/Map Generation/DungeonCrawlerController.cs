using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    up = 0,
    left = 1,
    down = 2,
    right = 3
}

public class DungeonCrawlerController : MonoBehaviour
{
    public static List<Vector2Int> positionsVisited = new List<Vector2Int>();

    private static readonly Dictionary<Direction, Vector2Int> _directionMovementMap = new Dictionary<Direction, Vector2Int>
    {
        {Direction.up, Vector2Int.up },
        {Direction.left, Vector2Int.left },
        {Direction.down, Vector2Int.down },
        {Direction.right, Vector2Int.right }
    };

    public static List<Vector2Int> GenerateDungeon(DungeonGenerationData dungeonData)
    {
        List<DungeonCrawler> dungeonCrawlers = new List<DungeonCrawler>();
    
        for(int i = 0; i < dungeonData.NumberOfCrawlers; i++)
        {
            dungeonCrawlers.Add(new DungeonCrawler(Vector2Int.zero));
        }

        int iterations = Random.Range(dungeonData.IterationMin, dungeonData.IterationMax);

        // StartPos
        positionsVisited.Add(Vector2Int.zero);

        for(int i = 0; i < iterations; i++)
        {
            foreach(DungeonCrawler crawler in dungeonCrawlers) 
            {
                Vector2Int newPos = crawler.Move(_directionMovementMap);

                while (positionsVisited.Contains(newPos))
                {
                    newPos = crawler.Move(_directionMovementMap);
                }

                positionsVisited.Add(newPos);
            }
        }

        return positionsVisited;
    }
}
