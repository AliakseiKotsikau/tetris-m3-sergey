using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public struct TetrominoData
{
    public TetrominoType type;
    private Vector2Int[] cells;
    private Vector2Int[,] wallKicks;

    public Vector2Int[] Cells => cells;
    public Vector2Int[,] WallKicks => wallKicks;

    public TetrominoData(TetrominoType type) : this()
    {
        this.type = type;
        cells = Data.Cells[type];
        wallKicks = Data.WallKicks[type];
    }
}
