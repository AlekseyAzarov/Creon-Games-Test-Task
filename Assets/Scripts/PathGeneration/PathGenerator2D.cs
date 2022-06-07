using System.Collections.Generic;
using UnityEngine;

public class PathGenerator2D : IPathGenerator
{
    public GeneratedPathData GeneratePath(PathData data, Vector3 start, Vector3 end)
    {
        var path = new List<Vector3>();
        path.Add(new Vector3(start.x, start.z));
        float distanceLeft = Vector3.Distance(start, end);

        while (distanceLeft > 0)
        {
            float segmentLenght = Random.Range(data.MinSegmentLength, data.MaxSegmentLength);
            float segmentSlope = Random.Range(data.MinPathSlope, data.MaxPathSlope);

            if (segmentLenght > distanceLeft) break;

            var prevPoint = path[path.Count - 1];

            var newPoint = new Vector3
            {
                x = Mathf.Cos(segmentSlope),
                y = prevPoint.y += segmentLenght,
                z = prevPoint.z
            };

            distanceLeft -= segmentLenght;
            path.Add(newPoint);
        }

        path.Add(new Vector3(end.x, end.z));

        var go = new GeneratedPathData
        {
            Path = path.ToArray(),
            Thickness = data.PathThickness
        };

        return go;
    }
}
