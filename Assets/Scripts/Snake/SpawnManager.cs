using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject applePrefab;

    private float xPos;
    private float yPos;

    public void SpawnFood()
    {
        Instantiate(applePrefab, SpawnPos(), transform.rotation);
    }

    private Vector3 SpawnPos()
    {
        xPos = Random.Range(-5, 5) * 0.225f;
        yPos = Random.Range(-5, 5) * 0.225f;
        return new Vector3(xPos, yPos, -1);
    }
}
