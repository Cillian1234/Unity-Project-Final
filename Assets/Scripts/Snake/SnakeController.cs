using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnakeController : MonoBehaviour
{
    public static SnakeController Instance;
    public GameObject snakeBody;

    public float speed;
    public Vector3 headPosition;
    private Vector3 lastMove;
    private Vector3 facing;
    public List<Vector3> bodyPositions = new List<Vector3>();
    private GameObject[] numOfBodies;

    private void Start()
    {
        Instance = GetComponent<SnakeController>();
        headPosition = new Vector3(0, 0, -1.5f);
        lastMove = new Vector3(0, 1, 0);
        facing = transform.eulerAngles;
        InvokeRepeating("MoveHandler", 1 , speed);
    }

    private void Update()
    {
        InputHandler();
    }

    private void MoveHandler()
    {
        bodySpawner();
        headPosition += lastMove;
        transform.position = headPosition;
        transform.eulerAngles = facing;

    }

    private void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.W) && lastMove != Vector3.down)
        {
            lastMove = Vector3.up;
            facing.z = 0;
        }
        if (Input.GetKeyDown(KeyCode.A) && lastMove != Vector3.right)
        {
            lastMove = Vector3.left;
            facing.z = 90;
        }
        if (Input.GetKeyDown(KeyCode.S) && lastMove != Vector3.up)
        {
            lastMove = Vector3.down;
            facing.z = 180;
        }
        if (Input.GetKeyDown(KeyCode.D) && lastMove != Vector3.left)
        {
            lastMove = Vector3.right;
            facing.z = 270;
        }
    }

    private void bodySpawner()
    {
        numOfBodies = GameObject.FindGameObjectsWithTag("SnakeBody");
        for (int i = 0; i < numOfBodies.Length; i++)
        {
            Destroy(numOfBodies[i]);
        }

        bodyPositions.Insert(0, headPosition);

        for (int i = 0; i < SpawnManager.Instance.fruitEaten; i++)
        {
            Instantiate(snakeBody, bodyPositions[i], transform.rotation);
        }
    }

    public List<Vector3> getAreaCoveredBySnake()
    {
        List<Vector3> snakeArea = new List<Vector3>() { headPosition };
        snakeArea.AddRange(bodyPositions);
        return snakeArea;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other);
        SpawnManager.Instance.fruitEaten++;
        SpawnManager.Instance.SpawnFood();
    }
}
