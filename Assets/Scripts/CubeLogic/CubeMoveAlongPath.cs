using UnityEngine;

public class CubeMoveAlongPath : IObjectMove
{
    private float _speed;
    private float _rotationSpeed;

    public CubeMoveAlongPath(float speed, float rotationSpeed)
    {
        _speed = speed;
        _rotationSpeed = rotationSpeed;
    }

    public void MoveWithRotation(Vector3 moveTo, Transform targetObject)
    {
        Vector3 moveDirection = moveTo - targetObject.position;
        moveDirection.Normalize();
        Quaternion rotateTo = Quaternion.LookRotation(Vector3.forward, moveDirection);
        targetObject.rotation = Quaternion.RotateTowards(targetObject.rotation, rotateTo, _rotationSpeed * Time.deltaTime);

        targetObject.Translate(moveDirection * _speed * Time.deltaTime, Space.World);
    }
}
