using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileSwaper : MonoBehaviour
{
    [SerializeField]
    private Tilemap tilemap;

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

        // Try to get a tile from cell position
        Tile firstTile = tilemap.GetTile<Tile>(firstTilePos);
        Tile secondTile = tilemap.GetTile<Tile>(secondTilePos);


        if (firstTile && secondTile)
        {
            tilemap.SetTile(secondTilePos, firstTile);
            tilemap.SetTile(firstTilePos, secondTile);
        }
    }

    //private IEnumerator SwapTiles(Tile first, Tile second, Vector3Int firstTilePos, Vector3Int secondTilePos)
    //{
    //    float swapTime = 0.5f;
    //    float time = 0f;

    //    float percent = 0f;


    //    Vector3 startFirst = first.gameObject.transform.position;
    //    Vector3 startSecond = second.gameObject.transform.position;

    //    Vector3 targetFirst = second.gameObject.transform.position;
    //    Vector3 targetSecond = first.gameObject.transform.position;

    //    while(percent < 1f)
    //    {
    //        first.gameObject.transform.position = Vector3.Lerp(startFirst, targetFirst, percent);
    //        first.gameObject.transform.position = Vector3.Lerp(startSecond, targetSecond, percent);
    //        time += Time.deltaTime;

    //        percent = time / swapTime;

    //        yield return new WaitForEndOfFrame();
    //    }

    //    tilemap.SetTile(secondTilePos, first);
    //    tilemap.SetTile(firstTilePos, second);
    //}

    //private float CalculateAngle()
    //{
    //    return Mathf.Atan2(finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 180 / Mathf.PI;
    //}
}
