using System;
using UnityEngine;

public class CubeHandler : MonoBehaviour
{
    public event Action CubeTraveledPath
    {
        add => _cubeMovement.LastPointReached += value;
        remove => _cubeMovement.LastPointReached -= value;
    }

    [SerializeField] private CubeMovement _cubeMovement;
    [SerializeField] private float _cubeSpeed;
    [SerializeField] private float _cubeRotationSpeed;

    public void InitCube(Vector3[] path)
    {
        var pathProvider = new Cube2DPathProvider()
        {
            Path = path
        };

        var cubeMove = new CubeMoveAlongPath(_cubeSpeed, _cubeRotationSpeed);

        IPlayerInput playerInput = null;

        if (SystemInfo.deviceType == DeviceType.Desktop) playerInput = new PcPlayerInput();
        else if (SystemInfo.deviceType == DeviceType.Handheld) playerInput = new HandheldPlayerInput();

        _cubeMovement.gameObject.SetActive(true);
        _cubeMovement.Init(playerInput, cubeMove, pathProvider);
    }

    public void UpdateCube() => _cubeMovement.UpdateCube();
}
