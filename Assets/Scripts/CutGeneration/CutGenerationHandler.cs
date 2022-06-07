using UnityEngine;

public class CutGenerationHandler : MonoBehaviour
{
    [SerializeField] private AbstractCutGenerator _cutGenerator;

    private Vector3[] vector3s;

    public void Init()
    {
        _cutGenerator.Init();
    }

    public void GenerateCut(Vector3[] cutPoints, float cutSize)
    {
        vector3s = cutPoints;
        _cutGenerator.GenerateCut(cutPoints, cutSize);
    }

    private void OnDrawGizmos()
    {
        if (vector3s == null) return;

        Gizmos.color = Color.red;

        foreach (var v in vector3s)
        {
            Gizmos.DrawSphere(v, 0.1f);
        }
    }
}
