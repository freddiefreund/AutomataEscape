using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class StateChartRunner : MonoBehaviour
{
    private GridManager _gridManager;
    private (int, int) _currentCoordinates;

    public void SetStartCoordinates((int, int) coordinates)
    {
        _currentCoordinates = coordinates;
        _gridManager = FindObjectOfType<GridManager>();
    }

    public IEnumerator Run()
    {
        while(true)
        {
            var dirAngle = (int)transform.rotation.eulerAngles.z;
            Debug.Log($"The Runner was called and has following angle: {dirAngle}");
            switch(dirAngle)
            {
                case 0:
                    Move(new Vector2Int(0, -1));
                    break;
                case 90:
                    Move(new Vector2Int(1, 0));
                    break;
                case -90:
                    Move(new Vector2Int(-1, 0));
                    break;
                case 180:
                    Move(new Vector2Int(0, 1));
                    break;
            } 
            yield return new WaitForSeconds(0.3f);   
        }
    }

    private void Move(Vector2Int dir)
    {
        var newCoordinates = (_currentCoordinates.Item1 + dir.x, _currentCoordinates.Item2 + dir.y);
        Debug.Log($"New Coordinates are: {newCoordinates}");
        if (_gridManager.Grid.ContainsKey(newCoordinates))
        {
            transform.position = _gridManager.Grid[newCoordinates].transform.position;
            _currentCoordinates = newCoordinates;
        }
    }

    private void Turn(int deg)
    {
        
    }
}
