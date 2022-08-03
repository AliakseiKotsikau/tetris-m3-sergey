using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Board))]
public class Ghost : MonoBehaviour
{
    [SerializeField]
    private Tile tile;
    [SerializeField]
    private Board mainBoard;

    private Board ghostBoard;

    private Tetromino trackingPiece;
    private Vector3Int[] ghostCells;
    private Vector3Int ghostPosition;

    private void Awake()
    {
        ghostBoard = GetComponent<Board>();
        ghostCells = new Vector3Int[4];
    }

    private void LateUpdate()
    {
        ghostBoard.ClearTiles(ghostCells, ghostPosition);
        CopyTrackingPieceTiles();
        Drop();
        ghostBoard.SetTiles(ghostCells, ghostPosition, tile);
    }

    private void Drop()
    {
        Vector3Int position = trackingPiece.Position;

        int current = position.y;
        int bottom = -ghostBoard.BoardSize.y / 2 - 1;

        mainBoard.ClearPieceTiles(trackingPiece);

        for (int row = current; row >= bottom; row--)
        {
            position.y = row;

            if (mainBoard.IsValidPosition(trackingPiece, position))
            {
                ghostPosition = position;
            }
            else
            {
                break;
            }
        }

        mainBoard.SetTiles(trackingPiece);
    }

    private void CopyTrackingPieceTiles()
    {
        // делаем так, чтобы очистить старые тайлы и только потом получить новое положение
        for (int i = 0; i < ghostCells.Length; i++)
        {
            ghostCells[i] = trackingPiece.Cells[i];
        }
    }

    public void SetTrackingPiece(Tetromino piece)
    {
        trackingPiece = piece;
    }
}
