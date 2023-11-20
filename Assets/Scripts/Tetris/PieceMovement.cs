using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMovement : MonoBehaviour
{
    private GameObject activePiece;
    // Start is called before the first frame update
    void Start()
    {
        activePiece = GameObject.FindGameObjectWithTag("Tetris.Active");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
