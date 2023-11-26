using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private MoveDown movePiece;
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

    // Start is called before the first frame update
    void Start()
    {
        spawnNextPiece();
        findPiece();
        getActivePieceChildPositions();
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
            lastMove = Vector3.left;
            activePiece.transform.SetPositionAndRotation(move, activePiece.transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Slammed piece");
        }

        if (Input.GetKeyDown(KeyCode.D) && checkIfPieceInBounds(false))
        {
            move = activePiece.transform.position + Vector3.right;
            lastMove = Vector3.right;
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
                lastMove = Vector3.up;
                activePiece.tag = "Tetris.Inactive";
                CancelInvoke("MovePieceDown");
                return false;
            }
        }

        return true;
    }
}
