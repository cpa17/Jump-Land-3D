using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    [SerializeField] private NavMeshAgent enemy;

    // Update is called once per frame
    void Update()
    {
        enemy.SetDestination(GameObject.FindWithTag("Player").transform.position);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerLife.Health -= 1;
        } 
    }
}
