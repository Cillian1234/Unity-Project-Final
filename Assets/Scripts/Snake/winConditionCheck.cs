using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winConditionCheck : MonoBehaviour
{
    [SerializeField] private SpawnManager spawnManager;

    private int winCondition = 120;

    // Update is called once per frame
    void Update()
    {
        if (spawnManager.fruitEaten >= winCondition)
        {
            spawnManager.isGameOver = true;
        }
    }
}
