public abstract class AbstractGameState
{
    protected readonly PathGenerationHandler _pathGenerationHandler;
    protected readonly CutGenerationHandler _cutGenerationHandler;
    protected readonly PathStartEndPointsHandler _pathStartEndPointsHandler;
    protected readonly CubeHandler _cubeHandler;

    private readonly GameStateMachine _stateMachine;

    public AbstractGameState(PathGenerationHandler pathGenerationHandler,
        CutGenerationHandler cutGenerationHandler,
        PathStartEndPointsHandler pathStartEndPointsHandler,
        GameStateMachine gameStateMachine,
        CubeHandler cubeHandler)
    {
        _pathGenerationHandler = pathGenerationHandler;
        _cutGenerationHandler = cutGenerationHandler;
        _pathStartEndPointsHandler = pathStartEndPointsHandler;
        _stateMachine = gameStateMachine;
        _cubeHandler = cubeHandler;
    }

    public abstract void StateStart();
    public abstract void StateStop();

    public abstract void InState();

    protected void ChangeState<T>() where T : AbstractGameState
    {
        _stateMachine.ChangeState<T>();
    }
}
