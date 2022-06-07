public class GameplayState : AbstractGameState
{
    public GameplayState(PathGenerationHandler pathGenerationHandler,
        CutGenerationHandler cutGenerationHandler,
        PathStartEndPointsHandler pathStartEndPointsHandler,
        GameStateMachine gameStateMachine,
        CubeHandler cubeHandler)
        : base(pathGenerationHandler, cutGenerationHandler, pathStartEndPointsHandler, gameStateMachine, cubeHandler)
    { }

    public override void StateStart()
    {
        _cubeHandler.CubeTraveledPath += ChangeState<GameEndState>;
    }
    public override void InState()
    {
        _cubeHandler.UpdateCube();
    }

    public override void StateStop()
    {
        _cubeHandler.CubeTraveledPath -= ChangeState<GameEndState>;
    }
}
