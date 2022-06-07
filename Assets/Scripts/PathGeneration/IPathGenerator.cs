using UnityEngine;

public interface IPathGenerator
{
    GeneratedPathData GeneratePath(PathData data, Vector3 start, Vector3 end);
}
