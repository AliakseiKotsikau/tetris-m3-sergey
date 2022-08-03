using UnityEngine;
using UnityEngine.Tilemaps;

public class Tetromino : MonoBehaviour
{
    private TetrominoData tetrominoData;
    private Vector3Int[] cells;
    
    private Tile color;

    public Vector3Int[] Cells => cells;
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

        if (cells == null)
        {
            cells = new Vector3Int[tetrominoData.Cells.Length];
        }

        for (int i = 0; i < tetrominoData.Cells.Length; i++)
        {
            cells[i] = (Vector3Int)tetrominoData.Cells[i];
        }
    }

    public void Rotate(int direction)
    {
        RotationIndex = Wrap(RotationIndex + direction, 0, 4);

        for (int i = 0; i < cells.Length; i++)
        {
            Vector3 cellPosition = cells[i];

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

            cells[i] = new Vector3Int(x, y, 0);
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
