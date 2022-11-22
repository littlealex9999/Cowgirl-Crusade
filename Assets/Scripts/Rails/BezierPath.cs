using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierPath : MonoBehaviour
{
    [SerializeField] int bezierSmoothingPointsPerNode = 50;

    public List<Vector3> pathPoints;
    [HideInInspector] public List<Vector3> controlPoints;

    [SerializeField, HideInInspector] private Vector3[] calculatedBezier;
    public Vector3[] getPath { get { return calculatedBezier; } }

    private void Start()
    {
        
    }

    public void GeneratePath()
    {
        calculatedBezier = new Vector3[(pathPoints.Count - 1) * bezierSmoothingPointsPerNode];

        for (int i = 0; i < pathPoints.Count - 1; ++i) {
            int cp = i * 2;

            float t = 0f;
            Vector3 B = new Vector3(0, 0, 0);
            for (int j = 0; j < bezierSmoothingPointsPerNode; ++j) {
                B = (1 - t) * (1 - t) * (1 - t) * pathPoints[i] + 3 * (1 - t) * (1 - t) *
                    t * controlPoints[cp] + 3 * (1 - t) * t * t * controlPoints[cp + 1] + t * t * t * pathPoints[i + 1];

                calculatedBezier[i * bezierSmoothingPointsPerNode + j] = B;
                t += (1 / (float)bezierSmoothingPointsPerNode);
            }
        }
    }
}
