using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private MoveDown movePiece;
    public GameObject[] pieces;
    private GameObject[] allInactivePieces;

    private float leftBounds = -0.5f;
    private float rightBounds = 9.5f;
    private float bottomBounds = 0;
    private Vector3 childrenPositions;
    public List<Vector3> activePieceChildPositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        movePiece = GameObject.Find("Game Manager").GetComponent<MoveDown>();
        movePiece.piece.findPiece();
        movePiece.StartMoving();
    }

    // Update is called once per frame
    void Update()
    {
        movePiece.piece.findPiece();
        getActivePiecePositions();
        checkForPieceCollision();
        checkIfPieceInBounds();
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
            Instantiate(pieces[pieceNum], new Vector3(4.5f, 18, -1),Quaternion.identity).tag = "Tetris.Active";
        }
    }

    private void getActivePiecePositions() 
    {
        activePieceChildPositions.Clear();
        for (int i = 0; i < 4; i++)
        {
            activePieceChildPositions.Add(movePiece.piece.activePiece.transform.GetChild(i).transform.position);    
        }
    }

    private void checkIfPieceInBounds()
    {
        for (int i = 0; i < 4; i++)
        {
            
            if (activePieceChildPositions[i].x < leftBounds)
            {
                movePiece.piece.activePiece.transform.position += Vector3.right;
            }

            if (activePieceChildPositions[i].x > rightBounds)
            {
                movePiece.piece.activePiece.transform.position += Vector3.left;
            }

            if (activePieceChildPositions[i].y < bottomBounds)
            {
                movePiece.piece.activePiece.tag = "Tetris.Inactive";
                movePiece.CancelInvoke("MovePieceDown");
                spawnNextPiece();
                movePiece.piece.findPiece();
                movePiece.StartMoving();
            }
        }
    }

    private void checkForPieceCollision()
    {
        allInactivePieces = GameObject.FindGameObjectsWithTag("Tetris.Inactive");
        for (int i = 0; i < allInactivePieces.Length-1; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                if (activePieceChildPositions.Contains(allInactivePieces[i].transform.GetChild(j).transform.position))
                {
                    movePiece.piece.activePiece.transform.position += Vector3.up;
                    movePiece.piece.activePiece.tag = "Tetris.Inactive";
                    movePiece.CancelInvoke("MovePieceDown");
                    spawnNextPiece();
                    movePiece.piece.findPiece();
                    movePiece.StartMoving(); 
                }
            }
        }
    }
}
