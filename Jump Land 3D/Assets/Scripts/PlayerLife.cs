using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLife : MonoBehaviour
{
    public static int Health;
    private int _numberOfHearts;
    public GameObject endScreen;
    public GameObject cam;

    public Image[] hearts;

    private void Start()
    {
        Health = 3;
        _numberOfHearts = 5;
    }

    private void Update()
    {
        if (Health > _numberOfHearts)
        {
            Health = _numberOfHearts;
        }
        
        for (int i = 0; i < hearts.Length; i++)
        {

            if (i < Health)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        
        GameOver();
    }

    void GameOver()
    {
        if (Health == 0)
        {
            //Destroy(gameObject);
            cam.SetActive(false);
            endScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
