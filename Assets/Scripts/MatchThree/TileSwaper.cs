using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSwaper : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;
    [SerializeField]
    private MatchThreeGrid matchThreeGrid;

    private Vector2 firstTouchPosition;
    private Vector2 finalTouchPosition;


    private void OnMouseDown()
    {
        if (!GameManager.Instance.IsMatch3Mode()) return;

        firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseUp()
    {
        if (!GameManager.Instance.IsMatch3Mode()) return;

        finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (firstTouchPosition == finalTouchPosition)
        {
            return;
        }

        var firstTilePos = tilemap.WorldToCell(firstTouchPosition);
        var secondTilePos = tilemap.WorldToCell(finalTouchPosition);

        matchThreeGrid.SwapTiles(firstTilePos, secondTilePos);
    }

    //private void SwapTiles(Vector3Int firstTilePos, Vector3Int secondTilePos)
    //{
    //    Tile firstTile = tilemap.GetTile<Tile>(firstTilePos);
    //    Tile secondTile = tilemap.GetTile<Tile>(secondTilePos);

    //    if (firstTile && secondTile)
    //    {
    //        tilemap.SetTile(secondTilePos, firstTile);
    //        tilemap.SetTile(firstTilePos, secondTile);

    //        matchThreeGrid.SwapPieces(firstTilePos, secondTilePos);
    //        matchThreeGrid.FindMatches();
    //    }
    //}
}
