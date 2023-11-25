using System.Collections.Generic;
using System.ComponentModel.Design;
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
    public GameObject activePiece;

    private Vector3 move;
    private Quaternion rotation;
    private Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.1f;
        findPiece();
        StartMoving();
    }

    // Update is called once per frame
    void Update()
    {
        pieceMovement();
        findPiece();

        if (GameObject.FindGameObjectsWithTag("Tetris.Active").Length == 0)
        spawnNextPiece();
    }

    public void findPiece()
    {
        activePiece = GameObject.FindGameObjectWithTag("Tetris.Active");
    }

    private void MovePieceDown()
    {
        position = activePiece.transform.position + Vector3.down;
        activePiece.transform.SetPositionAndRotation(position, activePiece.transform.rotation);
    }

    public void StartMoving()
    {
        InvokeRepeating("MovePieceDown", 1, 1);
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
        findPiece();
        StartMoving();
    }

    private void pieceMovement() {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Swapped held piece");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            getActivePieceChildPositions();
            checkIfPieceInBounds();
            move = activePiece.transform.position + Vector3.left;
            activePiece.transform.SetPositionAndRotation(move, activePiece.transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Slammed piece");
        }

        if (Input.GetKeyDown(KeyCode.D) && (activePiece.transform.position + new Vector3(1, 0, 0)).x !< 9.5f)
        {
            getActivePieceChildPositions();
            checkIfPieceInBounds();
            move = activePiece.transform.position + new Vector3(1,0,0);
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

    private void checkIfPieceInBounds()
    {
        for (int i = 0; i < 4; i++)
        {
            if (activePieceChildPositions[i].x < leftBounds)
            {
                activePiece.transform.position += new Vector3(1, 0, 0);
            }

            if (activePieceChildPositions[i].x > rightBounds)
            {
                activePiece.transform.position -= new Vector3(1, 0, 0);
            }

            if (activePieceChildPositions[i].y < bottomBounds)
            {
                activePiece.tag = "Tetris.Inactive";
                CancelInvoke("MovePieceDown");
            }
        }
    }

    private void checkForPieceCollision()
    {

    }
}
