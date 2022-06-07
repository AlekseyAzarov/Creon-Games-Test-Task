using UnityEngine;

public class GameStartup : MonoBehaviour
{
    [SerializeField] private PathGenerationHandler _pathGenerationHandler;
    [SerializeField] private CutGenerationHandler _cutGenerationHandler;
    [SerializeField] private PathStartEndPointsHandler _pathStartEndPointsHandler;
    [SerializeField] private CubeHandler _cubeHandler;

    private GameStateMachine _gameStateMachine;

    private void Start()
    {
        _gameStateMachine = new GameStateMachine(_pathGenerationHandler,
            _cutGenerationHandler, _pathStartEndPointsHandler, _cubeHandler);
    }

    private void Update()
    {
        _gameStateMachine.InState();
    }
}
