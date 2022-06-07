using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class PathStartEndPointsHandler : MonoBehaviour
{
    private Vector3 _startPoint;
    private Vector3 _endPoint;

    public Vector3 StartPoint => _startPoint;
    public Vector3 EndPoint => _endPoint;

    public void Init()
    {
        var bounds = GetComponent<MeshFilter>().mesh.bounds;

        _startPoint = new Vector3(bounds.center.x, 0f, -bounds.max.z);
        _endPoint = new Vector3(bounds.center.x, 0f, bounds.max.z);
    }
}
