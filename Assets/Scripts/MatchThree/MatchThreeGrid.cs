using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Board))]
public class MatchThreeGrid : MonoBehaviour
{
    private Vector2Int boardSize;
    private Board board;

    private Piece[,] pieces;

    private MatchFinder matchFinder;

    // Start is called before the first frame update
    void Start()
    {
        matchFinder = new MatchFinder();
        board = GetComponent<Board>();
        board.PieceFellDown += UpdateFallenTilePosition;
        board.TilesMovedDown += FindMatchesWithoutSwap;

        boardSize = board.BoardSize;

        pieces = new Piece[boardSize.x, boardSize.y];
    }

    public void AddPieces(Piece[] newPieces)
    {
        foreach (var item in newPieces)
        {
            pieces[item.Position.x + boardSize.x / 2, item.Position.y + boardSize.y / 2] = item;
        }
    }

    public void SwapTiles(Vector3Int firstTilePos, Vector3Int secondTilePos)
    {
        bool swapped = board.SwapTiles(firstTilePos, secondTilePos);

        if (swapped)
        {
            SwapPieces(firstTilePos, secondTilePos);
            StartCoroutine(FindMatches(firstTilePos, secondTilePos));
        }
    }

    private void SwapPieces(Vector3Int firstTilePos, Vector3Int secondTilePos)
    {
        int firstX = firstTilePos.x + boardSize.x / 2;
        int firstY = firstTilePos.y + boardSize.y / 2;

        int secondX = secondTilePos.x + boardSize.x / 2;
        int secondY = secondTilePos.y + boardSize.y / 2;

        Piece temp = pieces[firstX, firstY];
        pieces[firstX, firstY] = pieces[secondX, secondY];
        pieces[secondX, secondY] = temp;

        SwapPiecePositions(pieces[firstX, firstY], pieces[secondX, secondY]);
    }

    private void SwapPiecePositions(Piece firstPiece, Piece secondPiece)
    {
        Vector3Int firstPiecePos = firstPiece.Position;
        firstPiece.Position = secondPiece.Position;
        secondPiece.Position = firstPiecePos;
    }

    private IEnumerator FindMatches(Vector3Int firstTilePos, Vector3Int secondTilePos)
    {
        yield return new WaitForSeconds(0.2f);
        List<Piece> matches = matchFinder.FindMatches(pieces);

        if (matches.Count == 0)
        {
            board.SwapTiles(firstTilePos, secondTilePos);
            SwapPieces(firstTilePos, secondTilePos);
        }
        else
        {
            foreach (var item in matches)
            {
                pieces[item.Position.x + boardSize.x / 2, item.Position.y + boardSize.y / 2] = null;
            }
            board.ClearTiles(matches);
        }
    }
    
    private void FindMatchesWithoutSwap()
    {
        List<Piece> matches = matchFinder.FindMatches(pieces);

        if (matches.Count > 0)
        {
            foreach (var item in matches)
            {
                pieces[item.Position.x + boardSize.x / 2, item.Position.y + boardSize.y / 2] = null;
            }
            board.ClearTiles(matches);
        }
    }



    private void UpdateFallenTilePosition(Vector3Int previousPosition, Vector3Int currentPosition)
    {
        Piece temp = pieces[previousPosition.x + boardSize.x / 2, previousPosition.y + boardSize.y / 2];
        pieces[currentPosition.x + boardSize.x / 2, currentPosition.y + boardSize.y / 2] = temp;
        temp.Position = currentPosition;
    }
}
