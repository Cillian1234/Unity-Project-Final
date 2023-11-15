using System;
using UnityEngine;
using System.Collections;

public class SnakeController : MonoBehaviour
{
    public float speed;
    private int directionPressed = 0;
    private Vector3 yMove = new Vector3(0, 0.225f, 0);
    private Vector3 xMove = new Vector3(0.225f, 0, 0);
    private SpawnManager spawnManager;
    public GameObject head, body;
    private int nChildObjects;
    public Vector3[] bodyPos = new Vector3[1];
    public GameObject[] bodyPieces = new GameObject[0];

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        InvokeRepeating(nameof(MoveForwards), 1, speed);
        bodyPieces[0] = GameObject.FindWithTag("SnakeBody");
        bodyPos[0] = bodyPieces[0].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            directionPressed = 0;
        } else if (Input.GetKeyDown(KeyCode.A))
        {
            directionPressed = 1;
        } else if (Input.GetKeyDown(KeyCode.S))
        {
            directionPressed = 2;
        } else if (Input.GetKeyDown(KeyCode.D))
        {
            directionPressed = 3;
        }
    }

    private void MoveForwards()
    {
        switch (directionPressed)
        {
            case 0:
                transform.Translate(yMove);
                break;
            case 1:
                transform.Translate(-xMove);
                break;
            case 2:
                transform.Translate(-yMove);
                break;
            case 3:
                transform.Translate(xMove);
                break;
        }

        for (int i = 0; i <= bodyPieces.Length-1; i++)
        {
            bodyPieces[i] = GameObject.FindGameObjectsWithTag("SnakeBody")[i];
            bodyPos[i].Set(bodyPieces[i].transform.position.x, bodyPieces[i].transform.position.y, -1.1f);
        }

        for (int i = 0; i < bodyPieces.Length; i++)
        {
                bodyPieces[i].transform.position = bodyPos[i];
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        spawnManager.SpawnFood();

        bodyPieces = new GameObject[bodyPieces.Length + 1];

        switch (directionPressed)
        {
            case 0:
                Instantiate(body, bodyPos[bodyPos.Length-1] - yMove, transform.rotation);
                break;
            case 1:
                Instantiate(body, bodyPos[bodyPos.Length-1] + xMove, transform.rotation);
                break;
            case 2:
                Instantiate(body, bodyPos[bodyPos.Length-1] + yMove, transform.rotation);
                break;
            case 3:
                Instantiate(body, bodyPos[bodyPos.Length-1] - xMove, transform.rotation);
                break;
        }
        bodyPos = new Vector3[bodyPieces.Length];
    }
}
