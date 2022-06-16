using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinSystem : MonoBehaviour
{
    public GameObject winScreen;
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        winScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
