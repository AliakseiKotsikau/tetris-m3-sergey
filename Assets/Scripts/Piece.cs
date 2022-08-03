using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Piece
{
    private Vector3Int cell;
    private Tile color;

    public Vector3Int Cell
    {
        get => cell;
        set { cell = value; }
    }
    public Tile Color => color;

    public Piece(Vector3Int cell, Tile color)
    {
        this.cell = cell;
        this.color = color;
    }
}
