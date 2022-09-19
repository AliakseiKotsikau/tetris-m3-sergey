using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceMover : MonoBehaviour
{
    [SerializeField]
    private Board board;
    [SerializeField]
    private float stepDelay = 1f;
    [SerializeField]
    private float lockDelay = 0.2f;

    private Tetromino activePiece;

    private float stepTime;
    private float lockTime;

    private void Start()
    {
        GameManager.Instance.ModeSwaped += OnStartMoving;
        GameManager.Instance.GameStarted += OnStartMoving;
    }

    private void Update()
    {
        if (!GameManager.Instance.IsTetrisMode()) return;

        lockTime += Time.deltaTime;

        if (Time.time >= stepTime)
        {
            Step();
        }
    }

    private void OnStartMoving()
    {
        stepTime = Time.time + stepDelay;
        lockTime = 0f;
    }

    public void SetActivePiece(Tetromino piece)
    {
        this.activePiece = piece;
    }

    public bool MoveActivePiece(Vector3Int translation)
    {
        Vector3Int newPosition = activePiece.Position + translation;

        bool isValidPosition = board.IsValidPosition(activePiece, newPosition);
        if (isValidPosition)
        {
            activePiece.Position = newPosition;
            lockTime = 0f;
        }

        return isValidPosition;
    }

    public void RotateActivePiece(int direction)
    {
        activePiece.Rotate(direction);

        if (!TestWallKicks(activePiece.RotationIndex, direction))
        {
            activePiece.Rotate(-direction);
        }
    }

    private bool TestWallKicks(int rotationIndex, int rotationDirection)
    {
        int wallKickIndex = activePiece.GetWallKickIndex(rotationIndex, rotationDirection);

        for (int i = 0; i < activePiece.TetrominoData.WallKicks.GetLength(1); i++)
        {
            Vector2Int translation = activePiece.TetrominoData.WallKicks[wallKickIndex, i];

            if (MoveActivePiece((Vector3Int)translation))
            {
                return true;
            }
        }

        return false;
    }

    public void HardDrop()
    {
        while (MoveActivePiece(Vector3Int.down))
        {
            continue;
        }

        board.Lock();
    }

    public void Step()
    {
        stepTime = Time.time + stepDelay;
        board.ClearActivePieceTiles();
        MoveActivePiece(Vector3Int.down);
        board.UpdateActivePieceTiles();

        if (lockTime >= lockDelay)
        {
            board.Lock();
        }
    }
}
