using UnityEngine;

[CreateAssetMenu(fileName = "Path Data", menuName = "Data/Path Data")]
public class PathDataScriptableObject : ScriptableObject
{
    [SerializeField, Range(-45, 45)] private float _minPathSlope;
    [SerializeField, Range(-45, 45)] private float _maxPathSlope;
    [SerializeField, Range(0, 2)] private float _minSegmentLength;
    [SerializeField, Range(0, 2)] private float _maxSegmentLength;
    [SerializeField] private float _pathThickness;

    public PathData GetPathData()
    {
        var pathData = new PathData
        {
            MinPathSlope = _maxPathSlope,
            MaxPathSlope = _minPathSlope,
            MinSegmentLength = _minSegmentLength,
            MaxSegmentLength = _maxSegmentLength,
            PathThickness = _pathThickness,
        };

        return pathData;
    }
}
