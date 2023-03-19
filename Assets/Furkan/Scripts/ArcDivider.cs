using UnityEngine;

public class ArcDivider : MonoBehaviour
{
    public Transform startPoint; // Starting point of the arc
    public Transform endPoint; // Ending point of the arc
    public int divisions; 
    public GameObject pointPrefab; 
    public float radius; 
    public float startPointModify;

    [ContextMenu("HandCalculate")]
    void Start()
    {
        DrawArc(startPoint.position, endPoint.position, divisions);
    }

    void DrawArc(Vector3 start, Vector3 end, int divisions)
    {
        Vector3 center = (start + end) / 2f; // Center of the arc
        float startAngle = 0 + startPointModify;// Starting angle of the arc
        float endAngle = 180 - startPointModify;
        

        for (int i = 0; i <= divisions; i++)
        {
            float currentAngle = Mathf.Lerp(startAngle, endAngle, (float)i / divisions); // Current angle of the division
            float x = center.x + radius * Mathf.Cos(currentAngle * Mathf.Deg2Rad); // X coordinate of the division
            float y = center.y + radius * Mathf.Sin(currentAngle * Mathf.Deg2Rad); // Y coordinate of the division
            Vector3 pointPos = new Vector3(x, y, 0); // Create a Vector3 point position with selected height
            Quaternion pointRot = Quaternion.Euler(0f, 0f, currentAngle - 90f); // Create a Quaternion point rotation
            GameObject point = Instantiate(pointPrefab, pointPos, pointRot); // Instantiate a new point
            point.transform.SetParent(transform); // Set the new point's parent to this object
        }
    }
}
