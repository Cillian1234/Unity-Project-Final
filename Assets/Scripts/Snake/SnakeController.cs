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
        if (headPosition == spawnManager.fruitPosition) // spawn new food if head is on current food
        {
            spawnManager.SpawnFood();
        }
        InputHandler(); // Check inputs every frame
    }

    private void MoveHandler()
    {
        BodySpawner(); // Finds, destroys and recreates body pieces every move
        headPosition += lastMove; // move head in direction pressed
        checkGameOver(); // check if you lost the game
        transform.position = headPosition; // move head
        transform.eulerAngles = facing; // change direction head is facing
    }

    private void InputHandler()
    {
        if (Input.GetKeyDown(KeyCode.W) && lastMove != Vector3.down) // do not allow snake to turn 180 degrees as the game would end immediately
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
        numOfBodies = GameObject.FindGameObjectsWithTag("SnakeBody"); // find all body pieces
        for (int i = 0; i < numOfBodies.Length; i++)
        {
            Destroy(numOfBodies[i]); // destroy all body pieces
        }

        bodyPositions.Insert(0, headPosition); // insert head position to top of arraylist

        if (bodyPositions.Count > numOfBodies.Length+1)
        {
            bodyPositions.RemoveAt(bodyPositions.Count-1); // remove the last position from arraylist as the snake has moved away from it
        }

        for (int i = 0; i < spawnManager.fruitEaten; i++)
        {
            Instantiate(snakeBody, bodyPositions[i], transform.rotation); // re-create all body pieces with new positions
        }
    }

    private void checkGameOver()
    {
        if (bodyPositions.Contains(headPosition) // if snake exits game bounds end game
            || headPosition.x > 5
            || headPosition.x < -5
            || headPosition.y > 5
            || headPosition.y < -5)
        {
            spawnManager.isGameOver = true;
            CancelInvoke("MoveHandler");
        }
    }

    public Vector3 getHeadPos()
    {
        return headPosition;
    }
}
