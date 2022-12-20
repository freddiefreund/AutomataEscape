using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChartManager : MonoBehaviour
{
    public enum StateAction
    {
        MoveForward,
        TurnLeft,
        TurnRight,
        Grab,
        None
    }
    
    public enum TransitionCondition
    {
        IsInFrontOfWall,
        StandsOnKey
    }
        
    public class Transition
    {
        public TransitionCondition Condition;
        public int DestinationId;

        public Transition(TransitionCondition condition, int destinationId)
        {
            Condition = condition;
            DestinationId = destinationId;
        }
    }

    public class StateData
    {
        public int StateId;
        public StateAction Action;
        public List<Transition> Transitions;
        public int DefaultTransitionDestinationId;

        public StateData(int stateId, StateAction action)
        {
            StateId = stateId;
            Action = action;
        }
    }

    public class StateChart
    {
        public StateData StartState;
        public List<StateData> ActiveStates;

        public StateChart()
        {
            StartState = new StateData(0, StateAction.None);
        }
    }
    
    public StateChart CurrentStateChart { get; private set; }

    private StateChartRunner _stateChartRunner;
    private (int, int) _robotPosition;

    public void InitializeStateChart(StateChartRunner runner, (int, int) robotPos)
    {
        CurrentStateChart = new StateChart();
        _stateChartRunner = runner;
        _robotPosition = robotPos;
    }

    public void ExecuteStateChartRunner()
    {
        _stateChartRunner.SetStartCoordinates(_robotPosition);
        StartCoroutine(_stateChartRunner.Run());
    }
}
