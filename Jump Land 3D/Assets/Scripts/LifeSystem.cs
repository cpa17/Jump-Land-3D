using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeSystem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (PlayerLife.Health < 5)
        {
            PlayerLife.Health += 1;
            Destroy(gameObject);
        }
        
    }
}
