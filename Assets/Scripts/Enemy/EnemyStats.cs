using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public int CurrentHealth;
    public int CurrentDamage;
    public int CurrentCoinAward;
    public float CurrentProjectileSpeed;

    [SerializeField] private EnemyData enemyData;

    private Transform _targetTransform;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = enemyData.EnemySprites[Random.Range(0, enemyData.EnemySprites.Length)];
    }

    void Start()
    {
        _targetTransform = GameObject.FindGameObjectWithTag("Cursor").transform;

        CurrentHealth = enemyData.Health;
        CurrentDamage = enemyData.Damage;
        CurrentCoinAward = enemyData.CoinAward;
        CurrentProjectileSpeed = enemyData.ProjectileSpeed;
    }
}
