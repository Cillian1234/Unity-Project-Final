
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.WSA;

public class PieceMovement : MonoBehaviour
{
    public GameObject activePiece;
    
    private Vector3 move;
    private Quaternion rotation;
    // Start is called before the first frame update
    public void findPiece()
    {
        activePiece = GameObject.FindGameObjectWithTag("Tetris.Active");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("Swapped held piece");
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            move = activePiece.transform.position + Vector3.left;
            activePiece.transform.SetPositionAndRotation(move, activePiece.transform.rotation);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Slammed piece");
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
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
}
