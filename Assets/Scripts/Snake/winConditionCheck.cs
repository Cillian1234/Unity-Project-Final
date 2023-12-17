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
        if (spawnManager.fruitEaten >= winCondition) // if snake length is 120 (entire game board) end the game
        {
            spawnManager.isGameOver = true;
        }
    }
}
