using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DungeonGenerationData.asset", menuName = "Game Datas/Dungeon Data")]
public class DungeonGenerationData : ScriptableObject
{
    public int NumberOfCrawlers;
    public int IterationMin;
    public int IterationMax;
}
