public class InitializationState : AbstractGameState
{
    public InitializationState(PathGenerationHandler pathGenerationHandler,
        CutGenerationHandler cutGenerationHandler,
        PathStartEndPointsHandler pathStartEndPointsHandler,
        GameStateMachine gameStateMachine,
        CubeHandler cubeHandler)
        : base(pathGenerationHandler, cutGenerationHandler, pathStartEndPointsHandler, gameStateMachine, cubeHandler)
    { }

    public override void StateStart()
    {
        _pathStartEndPointsHandler.Init();
        _pathGenerationHandler.Init();
        _cutGenerationHandler.Init();

        var path = _pathGenerationHandler.GeneratePath(_pathStartEndPointsHandler.StartPoint, _pathStartEndPointsHandler.EndPoint);
        _cutGenerationHandler.GenerateCut(path.Path, path.Thickness);

        _cubeHandler.InitCube(path.Path);

        ChangeState<GameplayState>();
    }

    public override void InState()
    {

    }

    public override void StateStop()
    {

    }
}
