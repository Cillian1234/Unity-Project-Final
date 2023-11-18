using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyCanvas : MonoBehaviour
{
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private SpawnManager spawnManager;

    // Update is called once per frame
    void Update()
    {
        if (spawnManager.isGameOver)
        {
            gameOverUI.SetActive(true);
            inGameUI.SetActive(false);
        } else if (!spawnManager.isGameOver)
        {
            gameOverUI.SetActive(false);
            inGameUI.SetActive(true);
        }
    }
}
