using UnityEngine;
using UnityEngine.Tilemaps;

public class Tetromino : MonoBehaviour
{
    private TetrominoData tetrominoData;
    private Tile color;
    private Piece[] pieces;

    public Piece[] Pieces => pieces;
    public Vector3Int Position { get; set; }
    public TetrominoData TetrominoData => tetrominoData;
    public int RotationIndex { get; set; }
    public Tile PieceColor => color;

    public void Init(Vector3Int position, TetrominoData tetrominoData, Tile[] tiles)
    {
        this.Position = position;
        this.tetrominoData = tetrominoData;
        this.RotationIndex = 0;
        color = tiles[0];

        if (pieces == null)
        {
            pieces = new Piece[tetrominoData.Cells.Length];
        }

        // create pieces with different colors
        for (int i = 0; i < pieces.Length; i++)
        {
            Vector3Int relativePositionInTetromino = (Vector3Int)tetrominoData.Cells[i];
            Vector3Int piecePositionOnBoard = relativePositionInTetromino + position;
            Tile tile = tiles[Random.Range(0, tiles.Length)];

            pieces[i] = new Piece(relativePositionInTetromino, piecePositionOnBoard, tile);
        }
    }

    public void UpdatePiecesPositions()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            Vector3Int relativePositionInTetromino = pieces[i].Cell;
            Vector3Int piecePositionOnBoard = relativePositionInTetromino + Position;

            pieces[i].Position = piecePositionOnBoard;
        }
    }

    public void Rotate(int direction)
    {
        RotationIndex = Wrap(RotationIndex + direction, 0, 4);

        for (int i = 0; i < pieces.Length; i++)
        {
            Vector3 cellPosition = pieces[i].Cell;

            int x, y;

            if (tetrominoData.type == TetrominoType.I || tetrominoData.type == TetrominoType.O)
            {
                cellPosition.x -= 0.5f;
                cellPosition.y -= 0.5f;
                x = Mathf.CeilToInt((cellPosition.x * Data.RotationMatrix[0] * direction) + (cellPosition.y * Data.RotationMatrix[1] * direction));
                y = Mathf.CeilToInt((cellPosition.x * Data.RotationMatrix[2] * direction) + (cellPosition.y * Data.RotationMatrix[3] * direction));
            }
            else
            {
                x = Mathf.RoundToInt((cellPosition.x * Data.RotationMatrix[0] * direction) + (cellPosition.y * Data.RotationMatrix[1] * direction));
                y = Mathf.RoundToInt((cellPosition.x * Data.RotationMatrix[2] * direction) + (cellPosition.y * Data.RotationMatrix[3] * direction));
            }

            pieces[i].Cell = new Vector3Int(x, y, 0);
        }
    }

    public int GetWallKickIndex(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = rotationIndex * 2;

        if(rotationDirection < 0)
        {
            wallKickIndex--;
        }

        return Wrap(wallKickIndex, 0, tetrominoData.WallKicks.GetLength(0));
    }

    private int Wrap(int input, int min, int max)
    {
        if (input < min)
        {
            return max - (min - input) % (max - min);
        }
        else
        {
            return min + (input - min) % (max - min);
        }
    }
}
