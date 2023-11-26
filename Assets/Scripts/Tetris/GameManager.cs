using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField]private GameObject[] pieces;
    private GameObject[] allInactivePieces;
    private float leftBounds = -1;
    private float rightBounds = 10f;
    private float bottomBounds = -0.5f;
    private Vector3 childrenPositions;
    private List<Vector3> activePieceChildPositions = new List<Vector3>();
    public GameObject activePiece;
    public Vector3 lastMove;
    private Vector3 move;
    private Quaternion rotation;
    private Vector3 position;
    private bool slowDownSlamming;

    // Start is called before the first frame update
    void Start()
    {
        spawnNextPiece();
        findPiece();
        getActivePieceChildPositions();

        slowDownSlamming = true;
    }

    // Update is called once per frame
    void Update()
    {
        pieceMovement();
        findPiece();

        if (GameObject.FindGameObjectsWithTag("Tetris.Active").Length == 0)
        spawnNextPiece();
        checkIfPieceInBounds(true);
    }

    public void findPiece()
    {
        activePiece = GameObject.FindGameObjectWithTag("Tetris.Active");
    }

    public void StartMoving()
    {
        InvokeRepeating("MovePieceDown", 1, 1);
    }
    private void MovePieceDown()
    {
        position = activePiece.transform.position + Vector3.down;
        activePiece.transform.SetPositionAndRotation(position, activePiece.transform.rotation);
    }


    private void spawnNextPiece()
    {
        int pieceNum = Random.Range(0, 7);
        if (pieces[pieceNum].CompareTag("Tetris.OnGrid"))
        {
            Instantiate(pieces[pieceNum], new Vector3(5f, 17.5f, -1),Quaternion.identity).tag = "Tetris.Active";
        }
        else
        {
            Instantiate(pieces[pieceNum], new Vector3(4.5f, 18f, -1),Quaternion.identity).tag = "Tetris.Active";
        }
        findPiece();
        StartMoving();
    }

    private void pieceMovement() {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Swapped held piece");
        }

        if (Input.GetKeyDown(KeyCode.A) && checkIfPieceInBounds(true))
        {
            move = activePiece.transform.position + Vector3.left;
            activePiece.transform.SetPositionAndRotation(move, activePiece.transform.rotation);
        }

        if (Input.GetKey(KeyCode.S) && slowDownSlamming)
        {
            move = activePiece.transform.position + Vector3.down;
            activePiece.transform.SetPositionAndRotation(move, activePiece.transform.rotation);

            slowDownSlamming = false;
            StartCoroutine("TimeForSlamming");
        }

        if (Input.GetKeyDown(KeyCode.D) && checkIfPieceInBounds(false))
        {
            move = activePiece.transform.position + Vector3.right;
            activePiece.transform.SetPositionAndRotation(move, activePiece.transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            activePiece.transform.Rotate(0,0,90f);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            activePiece.transform.Rotate(0,0,-90f);
        }
    }

    IEnumerator TimeForSlamming()
    {
        yield return new WaitForSeconds(0.25f);
        slowDownSlamming = true;
    }

    private void getActivePieceChildPositions()
    {
        activePieceChildPositions.Clear();
        for (int i = 0; i < 4; i++)
        {
            activePieceChildPositions.Add(activePiece.transform.GetChild(i).transform.position);
        }
    }

    private bool checkIfPieceInBounds(bool leftCheck)
    {
        getActivePieceChildPositions();

        if (leftCheck)
        {
            for (int i = 0; i < 4; i++)
            {
                if (activePieceChildPositions[i].x - 1 <= leftBounds)
                {
                    return false;
                }
            }
        }
        else
        {
            for (int i = 0; i < 4; i++)
            {
                if (activePieceChildPositions[i].x + 1 >= rightBounds)
                {
                    return false;
                }
            }
        }

        for (int i = 0; i < 4; i++)
        {
            if (activePieceChildPositions[i].y <= bottomBounds)
            {
                activePiece.tag = "Tetris.Inactive";
                CancelInvoke("MovePieceDown");
                return false;
            }
        }

        return true;
    }
}
