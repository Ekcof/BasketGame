using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] [Range(3, 30)] private int lineSegmentCount = 20;
    [SerializeField] private LayerMask ignoreLayer;
    private RaycastHit hit;
    private List<Vector3> linePoints = new List<Vector3>();

    #region Singleton
    public static DrawTrajectory Instance;

    private void Awake()
    {
        if (ignoreLayer == 0)
        {
            ignoreLayer = LayerMask.GetMask("Trigger");
        }
        Instance = this;
    }
    #endregion

    /// <summary>
    /// Update the trajectory of throw according to new position of throwing
    /// </summary>
    /// <param name="forceVector"></param>
    /// <param name="rigidbody"></param>
    /// <param name="startingPoint"></param>
    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidbody, Vector3 startingPoint)
    {
        Debug.DrawLine(startingPoint, forceVector);
        Vector3 velocity = (-forceVector / rigidbody.mass) * Time.fixedDeltaTime;

        float flightDuration = Mathf.Abs((2 * velocity.y) / Physics.gravity.y);

        float stepTime = flightDuration / lineSegmentCount;

        linePoints.Clear();

        for (int i = 0; i < lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i;
            Vector3 MovementVector = new Vector3(
            velocity.x * stepTimePassed,
            velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
            velocity.z * stepTimePassed
            );

            Vector3 newPoint = -MovementVector + startingPoint;
            if (linePoints.Count > 0)
            {
                if (Physics.Raycast(linePoints[i - 1], newPoint - linePoints[i - 1], out hit, (newPoint - linePoints[i - 1]).magnitude, ~ignoreLayer))
                {
                    linePoints.Add(hit.point);
                    break;
                }
            }

            linePoints.Add(newPoint);
        }

        lineRenderer.positionCount = linePoints.Count;
        lineRenderer.SetPositions(linePoints.ToArray());

    }
}
