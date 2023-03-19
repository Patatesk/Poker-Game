using System.Collections.Generic;
using UnityEngine;

public class ArcDeneme : MonoBehaviour
{
    public Transform startPoint; // Starting point of the arc
    public Transform endPoint; // Ending point of the arc
    public float arcAngle; // Angle of the arc in degrees
    public int divisions; // Number of divisions to create
    public GameObject pointPrefab; // Prefab to use for each point
    public float height; // Height of each point
    public float radius; // Radius of the arc
    public int startDivision; // Starting division for the arc
    public int endDivision; // Ending division for the arc
    List <GameObject> points = new List<GameObject>();
    [ContextMenu("Debnee")]
    void Start()
    {
        DrawArc(startPoint.position, endPoint.position, arcAngle, divisions,arcAngle,height);
    }

    void DrawArc(Vector3 start, Vector3 end, float angle, int divisions, float divisionAngle, float height)
    {
        Vector3 center = (start + end) / 2f; // Center of the arc
        float startAngle = Mathf.Atan2(start.y - center.y, start.x - center.x) * Mathf.Rad2Deg; // Starting angle of the arc
        float endAngle = Mathf.Atan2(end.y - center.y, end.x - center.x) * Mathf.Rad2Deg; // Ending angle of the arc
        float totalAngle = endAngle - startAngle; // Total angle of the arc
        float angleStep = divisionAngle; // Angle between each division
        float currentAngle = 0f; // Current angle of the division
        Vector3 pointPos = Vector3.zero; // Position of the current point
        Quaternion pointRot = Quaternion.identity; // Rotation of the current point

        // Create the first point at the start position with 0 rotation
        

        for (int i = 0; i <= divisions; i++)
        {
            // Calculate the position and rotation of the current point
            if (i % 2 == 0)
            {
                // Even division (to the right of the first point)
                currentAngle = startAngle + angleStep * (i / 2);
                pointPos = new Vector3(start.x + (radius * Mathf.Cos(currentAngle * Mathf.Deg2Rad)), start.y + (radius * Mathf.Sin(currentAngle * Mathf.Deg2Rad)), height);
                pointRot = Quaternion.Euler(0f, 0f, currentAngle - 90f);
            }
            else
            {
                // Odd division (to the left of the first point)
                currentAngle = endAngle - angleStep * ((i - 1) / 2);
                pointPos = new Vector3(start.x + (radius * Mathf.Cos(currentAngle * Mathf.Deg2Rad)), start.y + (radius * Mathf.Sin(currentAngle * Mathf.Deg2Rad)), height);
                pointRot = Quaternion.Euler(0f, 0f, currentAngle - 90f);
            }

            // Create the current point
            GameObject point = Instantiate(pointPrefab, pointPos, pointRot);
            point.transform.SetParent(transform);
            Debug.Log("Point " + i + ": " + pointPos);
        }
    }


}
