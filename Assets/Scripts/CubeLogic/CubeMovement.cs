using System;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    public event Action LastPointReached;

    private IPathUser _pathProvider;
    private IPlayerInput _playerInput;
    private IObjectMove _objectMove;

    private int _currentPathIndex;

    public void Init(IPlayerInput playerInput, IObjectMove objectMove, IPathUser pathUser)
    {
        _playerInput = playerInput;
        _objectMove = objectMove;
        _pathProvider = pathUser;

        _currentPathIndex = 0;

        transform.position = _pathProvider.Path[0];
    }

    public void UpdateCube()
    {
        if (Vector3.Distance(transform.position, _pathProvider.Path[_currentPathIndex]) <= 0.1f)
        {
            if (_currentPathIndex == _pathProvider.Path.Length - 1)
            {
                LastPointReached?.Invoke();
                return;
            }

            _currentPathIndex++;
        }

        var input = _playerInput.GetInput();
        Vector3 inputDir = new Vector3(input.x, input.y, 0f);

        _objectMove.MoveWithRotation(_pathProvider.Path[_currentPathIndex] + inputDir, transform);
    }
}
