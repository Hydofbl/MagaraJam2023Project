using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Room Room;
    [SerializeField] private float health;
    
    public void GetHit(float hitDamage)
    {
        health -= hitDamage;

        if(health <= 0 )
        {
            health = 0;

            // Die
            Room.SpawnedEnemyCount--;
            Destroy(gameObject);
        }
    }
}
