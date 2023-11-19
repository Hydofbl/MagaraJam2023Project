using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [HideInInspector]
    public float CurrentHealth;
    [HideInInspector]
    public float CurrentDamage;
    [HideInInspector]
    public float CurrentCoinAward;
    [HideInInspector]
    public float CurrentProjectileSpeed;

    [SerializeField] private EnemyData enemyData;

    private Transform _targetTransform;

    void Start()
    {
        _targetTransform = GameObject.FindGameObjectWithTag("Cursor").transform;

        CurrentHealth = enemyData.Health;
        CurrentDamage = enemyData.Damage;
        CurrentCoinAward = enemyData.CoinAward;
        CurrentProjectileSpeed = enemyData.ProjectileSpeed;
    }
}
