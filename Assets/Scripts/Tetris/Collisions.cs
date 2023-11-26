using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Collisions : MonoBehaviour
{
    private GameManager gameManager;
    private Vector3 position;
    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }
    private void OnCollisionEnter(Collision other)
    {
        position = gameManager.activePiece.transform.position;
        position.y += 0.5f;
        gameManager.activePiece.transform.position = position;
        gameManager.activePiece.tag = "Tetris.Inactive";
        gameManager.CancelInvoke("MovePieceDown");
    }
}
