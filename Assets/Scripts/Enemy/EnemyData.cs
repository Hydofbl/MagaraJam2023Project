using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData.asset", menuName = "Game Datas/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public int Health;
    public int Damage;
    public int CoinAward;
    public float ProjectileSpeed;
}
