using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject spawnerObj;
    [SerializeField] private float interval;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject skull;
    [SerializeField] private GameObject star;

    private GameObject[] enemys = new GameObject[10];
    private bool allSpawned = false;
    private int _i = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(interval, spawnerObj));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        if (_i < 9)
        {
            yield return new WaitForSeconds(interval);
            GameObject newEnemy = Instantiate(enemy, spawnPoint.transform.position, Quaternion.identity);
            enemys[_i] = newEnemy;
            Debug.Log(enemys.GetValue(9));
            StartCoroutine(spawnEnemy(interval, enemy));
        }
        else
        {
            Debug.Log(enemys.GetValue(9));
            allSpawned = true;
        }
        _i++;
    }

    private void Update()
    {
        if (allSpawned)
        {
            star.SetActive(true);
        }
    }
}
