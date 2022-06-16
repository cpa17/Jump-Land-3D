using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLeftRight : MonoBehaviour
{
    [SerializeField] private Transform enemy;
    [SerializeField] private float change;
    private float start;
    
    void Start() {
        start = enemy.position.z;
        change = change * Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        float newPos = transform.position.z + change; 
        enemy.position = new Vector3(enemy.position.x, enemy.position.y, newPos);

        if ((newPos > start + 10) || (newPos < start - 10))
        {
            change = -1 * change;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerLife.Health -= 1;
    }
}
