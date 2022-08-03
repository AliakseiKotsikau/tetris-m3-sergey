using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Events;

public class Board : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private Vector2Int boardSize = new Vector2Int(10, 20);

    public Vector2Int BoardSize => boardSize;

    private Tetromino activePiece;

    public RectInt Bounds
    {
        get
        {
            Vector2Int position = new Vector2Int(-boardSize.x / 2, -boardSize.y / 2);
            return new RectInt(position, boardSize);
        }
    }

    public UnityAction PieceLocked;

    public void AddPiece(Tetromino piece)
    {
        activePiece = piece;
        SetTiles(activePiece);
    }

    public void Lock()
    {
        SetTiles(activePiece);
        ClearLines();
        Destroy(activePiece.gameObject);
        PieceLocked?.Invoke();
    }

    public void SetTiles(Vector3Int[] cells, Vector3Int position, Tile tile)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePosition = cells[i] + position;
            tilemap.SetTile(tilePosition, tile);
        }
    }
    
    public void SetTiles(Tetromino piece, Tile tile)
    {
        SetTiles(piece.Cells, piece.Position, tile);
    }

    public void SetTiles(Tetromino piece)
    {
        SetTiles(piece, piece.PieceColor);
    }

    public void UpdateActivePieceTiles()
    {
        SetTiles(activePiece);
    }

    public void ClearTiles(Vector3Int[] cells, Vector3Int position)
    {
        for (int i = 0; i < cells.Length; i++)
        {
            Vector3Int tilePosition = cells[i] + position;
            tilemap.SetTile(tilePosition, null);
        }
    }
    
    public void ClearPieceTiles(Tetromino piece)
    {
        ClearTiles(piece.Cells, piece.Position);
    }

    public void ClearActivePieceTiles()
    {
        ClearPieceTiles(activePiece);
    }

    public bool IsValidPosition(Tetromino piece, Vector3Int position)
    {
        for (int i = 0; i < piece.Cells.Length; i++)
        {
            Vector3Int tilePosition = piece.Cells[i] + position;

            if (tilemap.HasTile(tilePosition))
            {
                return false;
            }

            if (!Bounds.Contains((Vector2Int)tilePosition))
            {
                return false;
            }
        }

        return true;
    }

    public void ClearBoard()
    {
        tilemap.ClearAllTiles();
    }

    private void ClearLines()
    {
        int rowIndex = Bounds.yMin;

        while (rowIndex < Bounds.yMax)
        {
            if (IsLineFull(rowIndex))
            {
                ClearLine(rowIndex);
                MoveAllTilesOneLineDown(rowIndex);
            }
            else
            {
                // не повышаем индекс после очистки, т.к. мы опускаем все, что было сверху вниз на этот же индекс
                rowIndex++;
            }
        }
    }

    private void ClearLine(int rowIndex)
    {
        for (int colIndex = Bounds.xMin; colIndex < Bounds.xMax; colIndex++)
        {
            Vector3Int position = new Vector3Int(colIndex, rowIndex, 0);

            tilemap.SetTile(position, null);
        }
    }

    private void MoveAllTilesOneLineDown(int rowIndex)
    {
        while (rowIndex < Bounds.yMax)
        {
            for (int colIndex = Bounds.xMin; colIndex < Bounds.xMax; colIndex++)
            {
                Vector3Int aboveRowTilePosition = new Vector3Int(colIndex, rowIndex + 1, 0);
                TileBase aboveTile = tilemap.GetTile(aboveRowTilePosition);

                Vector3Int currentRowTilePosition = new Vector3Int(colIndex, rowIndex, 0);

                tilemap.SetTile(currentRowTilePosition, aboveTile);
            }

            rowIndex++;
        }
    }

    private bool IsLineFull(int rowIndex)
    {
        for (int colIndex = Bounds.xMin; colIndex < Bounds.xMax; colIndex++)
        {
            Vector3Int position = new Vector3Int(colIndex, rowIndex, 0);

            if (!tilemap.HasTile(position))
            {
                return false;
            }
        }

        return true;
    }
}
