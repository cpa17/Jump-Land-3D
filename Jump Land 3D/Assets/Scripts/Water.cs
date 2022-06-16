using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Water : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform start;
    
    public static Transform Checkpoint;
    

    private void Start()
    {
        Checkpoint = start;
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerLife.Health -= 1;
        player.transform.position = Checkpoint.transform.position;
    }
}
