using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TileSystemManager : MonoBehaviour
{
    public static TileSystemManager Instance;

    [SerializeField] private Tilemap walls;
    [SerializeField] private Tilemap floor;
    [SerializeField] private Tilemap utils;
    [SerializeField] private Tilemap chairs;

    private void Awake()
    {
        SetInstance();
    }

    private void SetInstance()
    {
        if (Instance == null)
        {
            Instance = this;
        }            
        else
        {
            Destroy(this);
            Debug.LogError("Duplicates");
        }
    }

    public (Vector3Int, TileData, bool) GetNeighboringUtilInDirection(Vector2 position, Direction direction)
    {
        Vector3Int cellPos = utils.WorldToCell(position);
        switch (direction)
        {
            case Direction.up:
                cellPos.y += 1;
                break;
            case Direction.down:
                cellPos.y -= 1;
                break;
            case Direction.left:
                cellPos.x -= 1;
                break;
            case Direction.right:
                cellPos.x += 1;
                break;
        }

        bool valid = false;
        TileBase tile = utils.GetTile(cellPos);
        TileData tileData = new();
        if (tile != null)
        {
            valid = true;            
            tile.GetTileData(cellPos, utils, ref tileData);            
        }
        return (cellPos, tileData, valid);
    }
}
