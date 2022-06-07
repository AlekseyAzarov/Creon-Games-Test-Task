using System.Collections.Generic;

public class GameStateMachine
{
    private List<AbstractGameState> _states;
    private AbstractGameState _currentState;

    public GameStateMachine(PathGenerationHandler pathGenerationHandler,
        CutGenerationHandler cutGenerationHandler,
        PathStartEndPointsHandler pathStartEndPointsHandler,
        CubeHandler cubeHandler)
    {
        _states = new List<AbstractGameState>
        {
            new InitializationState(pathGenerationHandler, cutGenerationHandler, pathStartEndPointsHandler, this, cubeHandler),
            new GameplayState(pathGenerationHandler, cutGenerationHandler, pathStartEndPointsHandler, this, cubeHandler),
            new GameEndState(pathGenerationHandler, cutGenerationHandler, pathStartEndPointsHandler, this, cubeHandler)
        };

        _currentState = _states[0];
        _currentState.StateStart();
    }

    public void InState() => _currentState.InState();

    public void ChangeState<T>() where T : AbstractGameState
    {
        _currentState.StateStop();
        _currentState = _states.Find(t => t is T);
        _currentState.StateStart();
    }
}
