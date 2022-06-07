public class GameEndState : AbstractGameState
{
    public GameEndState(PathGenerationHandler pathGenerationHandler,
        CutGenerationHandler cutGenerationHandler,
        PathStartEndPointsHandler pathStartEndPointsHandler,
        GameStateMachine gameStateMachine,
        CubeHandler cubeHandler)
        : base(pathGenerationHandler, cutGenerationHandler, pathStartEndPointsHandler, gameStateMachine, cubeHandler)
    { }

    public override void StateStart()
    {
        GameRestarter.ReloadCurrentScene();
    }
    public override void InState()
    {

    }

    public override void StateStop()
    {

    }
}
