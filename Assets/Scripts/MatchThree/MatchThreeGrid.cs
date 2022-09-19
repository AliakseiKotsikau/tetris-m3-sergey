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
        boardSize = board.BoardSize;

        pieces = new Piece[boardSize.x, boardSize.y];
    }

    public void AddPieces(Piece[] newPieces)
    {
        foreach (var item in newPieces)
        {
            Debug.Log((item.Position.x) + "     " + (item.Position.y));
            Debug.Log((item.Position.x + boardSize.x / 2) + "     " + (item.Position.y + boardSize.y / 2));
            pieces[item.Position.x + boardSize.x/2, item.Position.y + boardSize.y / 2] = item;
        }
    }

    public void SwapPieces(Vector3Int firstTilePos, Vector3Int secondTilePos)
    {
        int firstX = firstTilePos.x + boardSize.x / 2;
        int firstY = firstTilePos.y + boardSize.y / 2;
        
        int secondX = secondTilePos.x + boardSize.x / 2;
        int secondY = secondTilePos.y + boardSize.y / 2;

        Piece temp = pieces[firstX, firstY];
        pieces[firstX, firstY] = pieces[secondX, secondY];
        pieces[secondX, secondY] = temp;
    }

    public void FindMatches()
    {
        List<Piece> matches = matchFinder.FindMatches(pieces);

        foreach (var item in matches)
        {
            Debug.Log(item);
        }
    }
}
