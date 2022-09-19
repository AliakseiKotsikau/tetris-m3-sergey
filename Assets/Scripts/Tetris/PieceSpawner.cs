using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// В целях уменьшения использования память, можно не спавнить кусочек каждый раз,
/// а использовать один и тот же, лишь изменяя его форму и положение
/// Но это очень незначительное влияние
/// </summary>
[RequireComponent(typeof(PieceMover))]
public class PieceSpawner : MonoBehaviour
{
    [SerializeField]
    private Tetromino piecePrefab;
    [SerializeField]
    private Board board;
    [SerializeField]
    private Ghost ghost;
    [SerializeField]
    private Tile[] tiles;

    private PieceMover pieceMover;
    private TetrominoData[] tetrominos;

    private void Start()
    {
        InitTetrominos();

        pieceMover = GetComponent<PieceMover>();

        board.PieceLocked += SpawnPiece;
        GameManager.Instance.GameStarted += SpawnPiece;
        GameManager.Instance.TetrisModeEnabled += SpawnPiece;
    }

    private void SpawnPiece()
    {
        int random = UnityEngine.Random.Range(0, tetrominos.Length);
        TetrominoData tetrominoData = tetrominos[random];

        Tetromino piece = Instantiate(piecePrefab);
        piece.Init(Vector3Int.up * 8, tetrominoData, tiles);

        if(board.IsValidPosition(piece, piece.Position))
        {
            pieceMover.SetActivePiece(piece);
            board.AddPiece(piece);
            ghost.SetTrackingPiece(piece);
        }else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        board.ClearBoard();
        SpawnPiece();
    }

    private void InitTetrominos()
    {
        int length = Enum.GetNames(typeof(TetrominoType)).Length;

        tetrominos = new TetrominoData[length];

        for (int i = 0; i < length; i++)
        {
            TetrominoType type = ((TetrominoType[]) Enum.GetValues(typeof(TetrominoType)) )[i];
            tetrominos[i] = new TetrominoData(type);
        }
    }
}
