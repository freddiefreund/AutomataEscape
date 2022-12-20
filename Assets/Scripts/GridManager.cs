using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private Vector3 gridStartPosition;
    [SerializeField] private GameObject floorTile;

    public Dictionary<(int, int), GameObject> Grid => _grid;

    private Dictionary<(int, int), GameObject> _grid;

    public void CreateLevelBasedOnGrid(int[,] gridSource)
    {
        _grid = new Dictionary<(int, int), GameObject>();
        for (int y = 0; y < gridSource.GetLength(0); y++)
        {
            for (int x = 0; x < gridSource.GetLength(1); x++)
            {
                if (gridSource[y, x] == 1)
                {
                    var positionOffset = new Vector3(x, -y, 0);
                    var newTile = Instantiate(floorTile, gridStartPosition + positionOffset, Quaternion.identity, transform);
                    _grid.Add((x,y), newTile);
                }
            }
        }
    }
}
