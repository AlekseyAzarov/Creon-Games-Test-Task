using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaneCut : AbstractCutGenerator
{
    private List<int> _leftLastVerticiesIndexes;
    private List<int> _rightLastVerticiesIndexes;

    public override void GenerateCut(Vector3[] cutPoints, float cutSize)
    {
        _cutPoints = new List<Vector3>();
        foreach (var cutPoint in cutPoints)
        {
            _cutPoints.Add(new Vector3(cutPoint.x, 0f, cutPoint.y));
        }
        _cutPoints.Reverse();
        _cutSize = cutSize;
        _vertices = new List<Vector3>(_mesh.vertices);

        SplitVericies();

        _triangles.Clear();

        _leftLastVerticiesIndexes = FindLastVerticiesIndexes(_leftVertices);
        RecalculateTriangles(_leftVertices, 0);
        MoveVerticies(_leftVertices, true);
        AddNewVerticies(_leftVertices);
        MoveNewVerticies(_leftVertices, true);
        AddTriangles(_leftVertices, _leftLastVerticiesIndexes, _leftVertices.Count - _cutPoints.Count, 0, true);

        _rightLastVerticiesIndexes = FindLastVerticiesIndexes(_rightVerticies);
        RecalculateTriangles(_rightVerticies, _leftVertices.Count);
        MoveVerticies(_rightVerticies, false);
        AddNewVerticies(_rightVerticies);
        MoveNewVerticies(_rightVerticies, false);
        AddTriangles(_rightVerticies, _rightLastVerticiesIndexes, _rightVerticies.Count - _cutPoints.Count, _leftVertices.Count, false);

        _vertices.Clear();
        _vertices.AddRange(_leftVertices);
        _vertices.AddRange(_rightVerticies);

        RecalculateUvs(_vertices);

        _mesh.vertices = _vertices.ToArray();
        _mesh.triangles = _triangles.ToArray();
        _mesh.uv = _uvs.ToArray();

        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();
        _mesh.RecalculateTangents();
    }

    private void SplitVericies()
    {
        _leftVertices = new List<Vector3>();
        _rightVerticies = new List<Vector3>();

        foreach (var vert in _vertices)
        {
            if (vert.x <= 0f)
            {
                _leftVertices.Add(vert);
            }

            if (vert.x >= 0f)
            {
                _rightVerticies.Add(vert);
            }
        }
    }

    private void AddNewVerticies(List<Vector3> targetVerticies)
    {
        targetVerticies.AddRange(_cutPoints);
    }

    private void MoveVerticies(List<Vector3> targetVerticies, bool isLeftSide)
    {
        int leftSideFactor = isLeftSide == true ? -1 : 1;

        float xShift = _cutPoints.Max(t => t.x) > Mathf.Abs(_cutPoints.Min(t => t.x)) ? _cutPoints.Max(t => t.x)
            : Mathf.Abs(_cutPoints.Min(t => t.x));

        for (int i = 0; i < targetVerticies.Count; i++)
        {
            float xPos = targetVerticies[i].x + xShift * leftSideFactor;
            float yPos = targetVerticies[i].y;
            float zPos = targetVerticies[i].z;

            targetVerticies[i] = new Vector3(xPos, yPos, zPos);
        }
    }

    private void MoveNewVerticies(List<Vector3> targetVerticies, bool isLeftSide)
    {
        int leftSideFactor = isLeftSide == true ? -1 : 1;

        for (int i = 0; i < targetVerticies.Count; i++)
        {
            float xPos = targetVerticies[i].x + _cutSize * leftSideFactor / 2;
            float yPos = targetVerticies[i].y;
            float zPos = targetVerticies[i].z;

            targetVerticies[i] = new Vector3(xPos, yPos, zPos);
        }
    }

    private void RecalculateTriangles(List<Vector3> verticies, int addToIndex)
    {
        List<int> triangles = new List<int>();
        int nextRow = verticies.Count / 11;

        for (int i = 0; i < verticies.Count - nextRow; i++)
        {
            if (i % nextRow == (nextRow - 1)) continue;

            int A = i;
            int B = A + nextRow;
            int C = B + 1;
            int D = A + 1;

            triangles.Add(A + addToIndex);
            triangles.Add(B + addToIndex);
            triangles.Add(C + addToIndex);
            triangles.Add(A + addToIndex);
            triangles.Add(C + addToIndex);
            triangles.Add(D + addToIndex);
        }

        _triangles.AddRange(triangles);
    }

    private List<int> FindLastVerticiesIndexes(List<Vector3> verticies)
    {
        List<int> indexes = new List<int>(from vert in verticies
                                          where Mathf.Round(vert.x) == 0f
                                          select verticies.IndexOf(vert));

        return indexes;
    }


    // TODO: Fix triangulation
    private void AddTriangles(List<Vector3> verticies, List<int> lastVerticiesIndexes, int newVertAt,
        int addToIndex, bool isLeftSide)
    {
        List<int> triangles = new List<int>();
        Queue<int> lastVerts = new Queue<int>(lastVerticiesIndexes);

        bool isNewVertLargerLastVerts = verticies.Count - newVertAt > lastVerts.Count;

        if (isLeftSide == true)
        {
            if (isNewVertLargerLastVerts == false)
            {
                for (int i = newVertAt + 1; i < verticies.Count; i++)
                {
                    int A;
                    int B;
                    int C;
                    int D;

                    if (i == verticies.Count - 1)
                    {
                        int iterations = lastVerts.Count - 1;

                        for (int j = 0; j < iterations; j++)
                        {
                            if (j != iterations - 1)
                            {
                                A = lastVerts.Dequeue();
                                B = i - 2;
                                C = i - 1;
                                D = lastVerts.Peek();

                                triangles.Add(A + addToIndex);
                                triangles.Add(B + addToIndex);
                                triangles.Add(C + addToIndex);
                                triangles.Add(C + addToIndex);
                                triangles.Add(D + addToIndex);
                                triangles.Add(A + addToIndex);
                            }
                            else
                            {
                                A = lastVerts.Dequeue();
                                B = i - 1;
                                C = i;
                                D = lastVerts.Peek();

                                triangles.Add(A + addToIndex);
                                triangles.Add(B + addToIndex);
                                triangles.Add(C + addToIndex);
                                triangles.Add(C + addToIndex);
                                triangles.Add(D + addToIndex);
                                triangles.Add(A + addToIndex);
                            }
                        }

                        break;
                    }

                    A = lastVerts.Dequeue();
                    B = i - 1;
                    C = i;
                    D = lastVerts.Peek();

                    triangles.Add(A + addToIndex);
                    triangles.Add(B + addToIndex);
                    triangles.Add(C + addToIndex);
                    triangles.Add(C + addToIndex);
                    triangles.Add(D + addToIndex);
                    triangles.Add(A + addToIndex);
                }
            }
            else
            {
                for (int i = newVertAt + 1; i < verticies.Count; i++)
                {
                    int A;
                    int B;
                    int C;
                    int D;

                    if (lastVerts.Count > 1)
                    {
                        A = lastVerts.Dequeue();
                        B = i - 1;
                        C = i;
                        D = lastVerts.Peek();

                        triangles.Add(A + addToIndex);
                        triangles.Add(B + addToIndex);
                        triangles.Add(C + addToIndex);
                        triangles.Add(C + addToIndex);
                        triangles.Add(D + addToIndex);
                        triangles.Add(A + addToIndex);
                    }
                    else
                    {
                        A = lastVerts.Peek();
                        B = i - 1;
                        C = i;

                        triangles.Add(A + addToIndex);
                        triangles.Add(B + addToIndex);
                        triangles.Add(C + addToIndex);
                    }
                }
            }
        }
        else
        {
            if (isNewVertLargerLastVerts == false)
            {
                for (int i = newVertAt + 1; i < verticies.Count; i++)
                {
                    int A;
                    int B;
                    int C;
                    int D;

                    if (i == verticies.Count - 1)
                    {
                        int iterations = lastVerts.Count - 1;

                        for (int j = 0; j < iterations; j++)
                        {
                            if (j != iterations - 1)
                            {
                                A = i - 2;
                                B = lastVerts.Dequeue();
                                C = lastVerts.Peek();
                                D = i - 1;

                                triangles.Add(A + addToIndex);
                                triangles.Add(B + addToIndex);
                                triangles.Add(C + addToIndex);
                                triangles.Add(C + addToIndex);
                                triangles.Add(D + addToIndex);
                                triangles.Add(A + addToIndex);
                            }
                            else
                            {
                                A = i - 1;
                                B = lastVerts.Dequeue();
                                C = lastVerts.Peek();
                                D = i;

                                triangles.Add(A + addToIndex);
                                triangles.Add(B + addToIndex);
                                triangles.Add(C + addToIndex);
                                triangles.Add(C + addToIndex);
                                triangles.Add(D + addToIndex);
                                triangles.Add(A + addToIndex);
                            }
                        }

                        break;
                    }

                    A = i - 1;
                    B = lastVerts.Dequeue();
                    C = lastVerts.Peek();
                    D = i;

                    triangles.Add(A + addToIndex);
                    triangles.Add(B + addToIndex);
                    triangles.Add(C + addToIndex);
                    triangles.Add(C + addToIndex);
                    triangles.Add(D + addToIndex);
                    triangles.Add(A + addToIndex);
                }
            }
            else
            {
                for (int i = newVertAt + 1; i < verticies.Count; i++)
                {
                    int A;
                    int B;
                    int C;
                    int D;

                    if (lastVerts.Count > 1)
                    {
                        A = i - 1;
                        B = lastVerts.Dequeue();
                        C = lastVerts.Peek();
                        D = i;

                        triangles.Add(A + addToIndex);
                        triangles.Add(B + addToIndex);
                        triangles.Add(C + addToIndex);
                        triangles.Add(C + addToIndex);
                        triangles.Add(D + addToIndex);
                        triangles.Add(A + addToIndex);
                    }
                    else
                    {
                        A = i - 1;
                        B = lastVerts.Peek();
                        C = i;

                        triangles.Add(A + addToIndex);
                        triangles.Add(B + addToIndex);
                        triangles.Add(C + addToIndex);
                    }
                }
            }
        }

        _triangles.AddRange(triangles);
    }

    private void RecalculateUvs(List<Vector3> verticies)
    {
        _uvs = new List<Vector2>();

        float maxX = verticies.Max(t => Mathf.Abs(t.x));
        float maxY = verticies.Max(t => Mathf.Abs(t.z));

        foreach (var vert in verticies)
        {
            float xUv = (vert.x + maxX) / (maxX * 2);
            float yUv = (vert.z + maxY) / (maxY * 2);

            _uvs.Add(new Vector2(xUv, yUv));
        }
    }
}
