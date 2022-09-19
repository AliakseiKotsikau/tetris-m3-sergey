using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class Piece
{
    private Vector3Int cell;
    private Tile color;
    private Vector3Int position;

    public Vector3Int Cell
    {
        get => cell;
        set { cell = value; }
    }
    public Vector3Int Position
    {
        get => position;
        set { position = value; }
    }
    public Tile Color => color;

    public Piece(Vector3Int cell, Vector3Int position, Tile color)
    {
        this.cell = cell;
        this.position = position;
        this.color = color;
    }
}
