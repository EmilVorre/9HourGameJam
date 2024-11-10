using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TileSystemManager : MonoBehaviour
{
    public static TileSystemManager Instance;

    [SerializeField] private List<Tilemap> tilemaps;

    public List<Tilemap> Tilemaps => new(tilemaps);

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

    public List<TileBase> GetTiles(Vector2 position)
    {
        List<TileBase> tiles = new();
        foreach (Tilemap tilemap in tilemaps)
        {
            Vector3Int cellPos = tilemap.WorldToCell(position);
            TileBase tile = tilemap.GetTile(cellPos);
            if (tile != null) 
            {
                tiles.Add(tile);
            }
        }
        return tiles;
    }

    public List<TileBase> GetNeighboringTilesInDirection(Vector2 position, Direction direction)
    {
        List<TileBase> tiles = new();
        foreach (Tilemap tilemap in tilemaps)
        {
            Vector3Int cellPos = tilemap.WorldToCell(position);
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
            TileBase tile = tilemap.GetTile(cellPos);
            if (tile != null)
            {
                tiles.Add(tile);
            }
        }
        return tiles;
    }
}
