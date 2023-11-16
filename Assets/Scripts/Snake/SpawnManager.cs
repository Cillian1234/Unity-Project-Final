using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager Instance;
    public GameObject applePrefab;

    private int xRange;
    private int yRange;
    public Vector3 position;
    public int fruitEaten;

    private void Start()
    {
        Instance = GetComponent<SpawnManager>();
        fruitEaten = 0;
    }

    public void SpawnFood()
    {
        Instantiate(applePrefab, SpawnPos(), applePrefab.transform.rotation);
    }

    private Vector3 SpawnPos()
    {
        List<Vector3> list =
            new List<Vector3>(SnakeController.Instance.getAreaCoveredBySnake());

        do
        {
            xRange = Random.Range(-5, 6);
            yRange = Random.Range(-5, 6);
            position = new Vector3(xRange, -5, -1);
        } while (!list.Contains(position));

        return position;
    }
}
