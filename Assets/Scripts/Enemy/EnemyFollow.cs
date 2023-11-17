using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    private Transform targetTransform;

    void Start()
    {
        targetTransform = GameObject.FindGameObjectWithTag("Cursor").transform;
    }

    // Do other things
}
