using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class aktivateEndboss : MonoBehaviour
{
    [SerializeField] private GameObject spawner;
    private void OnTriggerEnter(Collider other)
    {
        spawner.SetActive(true);
    }
}
