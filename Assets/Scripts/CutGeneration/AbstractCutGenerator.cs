using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public abstract class AbstractCutGenerator : MonoBehaviour
{
    protected MeshFilter _meshFilter;
    protected List<Vector3> _vertices;
    protected List<Vector3> _leftVertices;
    protected List<Vector3> _rightVerticies;
    protected List<int> _triangles;
    protected List<Vector2> _uvs;
    protected List<Vector3> _cutPoints;
    protected Mesh _mesh;
    protected float _cutSize;

    public virtual void Init()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _mesh = _meshFilter.sharedMesh;

        _vertices = new List<Vector3>(_mesh.vertices);
        _triangles = new List<int>(_mesh.triangles);
        _uvs = new List<Vector2>(_mesh.uv);
    }

    public abstract void GenerateCut(Vector3[] cutPoints, float cutSize);
}
