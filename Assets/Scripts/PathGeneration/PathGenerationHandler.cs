using UnityEngine;

public class PathGenerationHandler : MonoBehaviour
{
    [SerializeField] private PathDataScriptableObject _pathData;

    private IPathGenerator _pathGenerator;

    public void Init()
    {
        _pathGenerator = new PathGenerator2D();
    }

    public GeneratedPathData GeneratePath(Vector3 start, Vector3 end)
    {
        return _pathGenerator.GeneratePath(_pathData.GetPathData(), start, end);
    }
}

public struct GeneratedPathData
{
    public Vector3[] Path;
    public float Thickness;
}
