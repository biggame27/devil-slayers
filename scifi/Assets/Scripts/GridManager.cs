using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GridManager : MonoBehaviour
{
    public PlayerController player;
    [SerializeField] private int width, height;

    [SerializeField] private Tile tilePrefab;
    [SerializeField] private float tileSize;
    [SerializeField] private float tileStartX, tileStartY;


    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for(int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                var spawnedTile = Instantiate(tilePrefab, new Vector2(i*tileSize+tileStartX,j*tileSize+tileStartY), Quaternion.identity, transform);
                spawnedTile.name = $"Tile {i} {j}";

            }
        }
    }
    /*
    void OnPoint(InputValue _point)
    {
        Vector2 point = _point.Get<Vector2>();
        point = new Vector2(point.x/0.16f, point.y/0.16f);
        int currTileX = Mathf.FloorToInt((float)point.x);
        int currTileY = Mathf.FloorToInt((float)point.y);
        Debug.Log(currTileX + " " + currTileY);
    }
    */
}
