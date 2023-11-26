using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    private GameManager gameManager;
    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    private void OnCollisionEnter(Collision other)
    {
        gameManager.activePiece.transform.position = gameManager.lastMove;
        gameManager.activePiece.tag = "Tetris.Inactive";
        gameManager.CancelInvoke("MovePieceDown");
    }
}
