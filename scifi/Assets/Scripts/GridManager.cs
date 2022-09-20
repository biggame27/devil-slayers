using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
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
}
