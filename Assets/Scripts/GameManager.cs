using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelData
{
    public (int,int) RobotStartPosition { get; }
    public Quaternion RobotStartRotation { get; }
    public int[,] Grid { get; }

    public LevelData((int, int) startPos, Quaternion startRot, int[,] grid)
    {
        RobotStartPosition = startPos;
        RobotStartRotation = startRot;
        Grid = grid;
    }
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private StateChartManager stateChartManager;
    [SerializeField] private GameObject robotObject;

    public readonly Quaternion Up = Quaternion.Euler(new Vector3(0, 0, 180f));
    public readonly Quaternion Down = Quaternion.Euler(new Vector3(0, 0, 0));
    public readonly Quaternion Right = Quaternion.Euler(new Vector3(0, 0, 90));
    public readonly Quaternion Left = Quaternion.Euler(new Vector3(0, 0, -90));
        
    private LevelData[] _levels;
    private StateChartRunner _stateChartRunner;
    
    void Start()
    {
        _levels = new []
        {
            new LevelData(
                (2, 0),
                Down,
                new [,]
                {
                    {0, 1, 1, 1},
                    {1, 1, 1, 0},
                    {0, 1, 1, 0}
                }
            ),
            new LevelData(
                (0, 0),
                Right,
                new [,]
                {
                    {1, 1, 1, 1, 0},
                    {0, 0, 1, 1, 0},
                    {1, 1, 1, 1, 0},
                    {1, 1, 1, 1, 0},
                    {1, 1, 1, 1, 0}
                }
            )
        };
        
        // Start Level
        int levelId = 1;
        gridManager.CreateLevelBasedOnGrid(_levels[levelId].Grid);
        GameObject startTile = gridManager.Grid[_levels[levelId].RobotStartPosition];
        Vector3 robotStartPositionOnGrid = startTile.transform.position;
        Quaternion robotStartRotation = _levels[levelId].RobotStartRotation;
        var robotInstance = Instantiate(robotObject, robotStartPositionOnGrid, robotStartRotation);
        stateChartManager.InitializeStateChart(robotInstance.GetComponent<StateChartRunner>(), _levels[levelId].RobotStartPosition);
        stateChartManager.ExecuteStateChartRunner();
    }
    
    void Update()
    {
        
    }
}
