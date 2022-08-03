using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PieceMover), typeof(PieceSpawner))]
public class InputController : MonoBehaviour
{
    [SerializeField]
    private Board board;

    private PieceMover pieceMover;

    private void Awake()
    {
        pieceMover = GetComponent<PieceMover>();
    }

    private void Update()
    {
        if (!Input.anyKeyDown) return;

        board.ClearActivePieceTiles();

        if (Input.GetKeyDown(KeyCode.A))
        {
            pieceMover.MoveActivePiece(Vector3Int.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            pieceMover.MoveActivePiece(Vector3Int.right);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            pieceMover.MoveActivePiece(Vector3Int.down);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            // rotate left
            pieceMover.RotateActivePiece(-1);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            // rotate right
            pieceMover.RotateActivePiece(1);
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            pieceMover.HardDrop();
        }

        board.UpdateActivePieceTiles();
    }
}
