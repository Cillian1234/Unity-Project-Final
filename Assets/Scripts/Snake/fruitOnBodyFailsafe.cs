using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitOnBodyFailsafe : MonoBehaviour
{
    public SpawnManager spawnManager;

    private void OnTriggerEnter(Collider other)
    {
        spawnManager.fruitEaten--;
        spawnManager.SpawnFood();
    }
}
