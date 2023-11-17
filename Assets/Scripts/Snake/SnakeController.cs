using UnityEngine;
using System.Collections.Generic;

public class SnakeController : MonoBehaviour
{
    public GameObject snakeBody;
    public SpawnManager spawnManager;

    public float secPerMove;
    [HideInInspector] public Vector3 headPosition;
    private Vector3 lastMove;
    private Vector3 facing;
    public List<Vector3> bodyPositions = new List<Vector3>();
    private GameObject[] numOfBodies;

    private void Start()
    {
        headPosition = new Vector3(0, 0, -1.5f);
        lastMove = new Vector3(0, 1, 0);
        facing = transform.eulerAngles;
        InvokeRepeating("MoveHandler", 1 , secPerMove);
    }

    private void Update()
    {
        if (headPosition == spawnManager.fruitPosition)
        {
            spawnManager.SpawnFood();
        }
        InputHandler();
    }

    private void MoveHandler()
    {
        BodySpawner();
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

    private void BodySpawner()
    {
        numOfBodies = GameObject.FindGameObjectsWithTag("SnakeBody");
        for (int i = 0; i < numOfBodies.Length; i++)
        {
            Destroy(numOfBodies[i]);
        }

        bodyPositions.Insert(0, headPosition);

        if (bodyPositions.Count > numOfBodies.Length+1)
        {
            bodyPositions.RemoveAt(bodyPositions.Count-1);
        }

        for (int i = 0; i < spawnManager.fruitEaten; i++)
        {
            Instantiate(snakeBody, bodyPositions[i], transform.rotation);
        }
    }

    public Vector3 getHeadPos()
    {
        return headPosition;
    }
}
