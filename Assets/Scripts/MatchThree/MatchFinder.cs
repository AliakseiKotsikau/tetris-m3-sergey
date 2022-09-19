using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class MatchFinder
{
    public List<Piece> FindMatches(Piece[,] tiles)
    {
        List<Piece> matchedTiles = new List<Piece>();
        List<Piece> tempMatchTiles = new List<Piece>();

        for (int column = 0; column < tiles.GetLength(0); column++)
        {
            for (int row = 0; row < tiles.GetLength(1); row++)
            {
                tempMatchTiles = CheckMatches(tiles, column, row);
                if (tempMatchTiles.Count > 0)
                {
                    matchedTiles.AddRange(tempMatchTiles);
                    //matchedTiles.AddRange(CheckBombs(tempMatchTiles, tiles));
                }
                tempMatchTiles.Clear();
            }
        }

        return matchedTiles.Distinct().ToList();
    }

    private List<Piece> CheckMatches(Piece[,] tiles, int column, int row)
    {
        List<Piece> matchedTiles = new List<Piece>();

        Piece selectedTile = tiles[column, row];

        if (column > 0 && column < tiles.GetLength(0) - 1)
        {
            Piece leftTile = tiles[column - 1, row];
            Piece rightTile = tiles[column + 1, row];

            matchedTiles.AddRange(CheckTileTypesMatch(selectedTile, leftTile, rightTile));
        }

        if (row > 0 && row < tiles.GetLength(1) - 1)
        {
            Piece upTile = tiles[column, row + 1];
            Piece downTile = tiles[column, row - 1];

            matchedTiles.AddRange(CheckTileTypesMatch(selectedTile, upTile, downTile));
        }

        return matchedTiles;
    }

    //private List<Tile> CheckBombs(List<Tile> matchedTiles, Tile[,] allTiles)
    //{
    //    if (matchedTiles == null || matchedTiles.Count == 0)
    //    {
    //        return new List<Tile>();
    //    }

    //    List<Tile> tilesToDestroy = new List<Tile>();

    //    foreach (Tile tile in matchedTiles)
    //    {
    //        if (tile.IsMatched) continue;

    //        tilesToDestroy.AddRange(CheckDirectionBombs(allTiles, tile));
    //    }

    //    // проверяем есть ли в уничтоженных шариках бомбы
    //    List<Tile> tilesDestroyedFromDestroyedBombs = CheckBombs(tilesToDestroy, allTiles);
    //    while (tilesDestroyedFromDestroyedBombs.Count > 0)
    //    {
    //        tilesToDestroy.AddRange(tilesDestroyedFromDestroyedBombs);
    //        tilesDestroyedFromDestroyedBombs = CheckBombs(tilesDestroyedFromDestroyedBombs, allTiles);
    //    }

    //    return tilesToDestroy;
    //}

    //private List<Tile> CheckDirectionBombs(Tile[,] tiles, Tile selectedTile)
    //{
    //    List<Tile> tilesToDestroy = new List<Tile>();

    //    if (selectedTile.BombType == BombType.ROW_BOMB)
    //    {
    //        tilesToDestroy = Enumerable.Range(0, tiles.GetLength(0))
    //            .Select(x => tiles[x, selectedTile.TileCoordinates.y])
    //            .ToList();
    //        selectedTile.IsMatched = true;
    //    }
    //    else if (selectedTile.BombType == BombType.COLUMN_BOMB)
    //    {
    //        tilesToDestroy = Enumerable.Range(0, tiles.GetLength(1))
    //            .Select(x => tiles[selectedTile.TileCoordinates.x, x])
    //            .ToList();
    //        selectedTile.IsMatched = true;
    //    }
    //    // оставляем бомбы не помеченными, чтобы их тоже проверить
    //    tilesToDestroy.FindAll(x => x.BombType == BombType.NOT_BOMB).ForEach(item => item.IsMatched = true);

    //    return tilesToDestroy;
    //}

    private List<Piece> CheckTileTypesMatch(Piece selectedTile, Piece neighbour1, Piece neighbour2)
    {
        if (selectedTile == null || neighbour1==null || neighbour2 == null)
        {
            return new List<Piece>();
        }

        if ((selectedTile.Color == neighbour1.Color) && (selectedTile.Color == neighbour2.Color))
        {
            return new List<Piece>() { neighbour1, selectedTile, neighbour2 };
        }

        return new List<Piece>();
    }

}
