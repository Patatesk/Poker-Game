using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParabolaDrawer : MonoBehaviour
{
    public RectTransform startPoint; // Ba�lang�� noktas�
    public RectTransform endPoint; // Biti� noktas�
    public int pointCount = 10; // Olu�turulacak nokta say�s�
    public float parabolaHeight = 2f; // Parabol�n y�ksekli�i

    public GameObject prefab; // Olu�turulacak prefab

    void Start()
    {
        Vector3 startPointPosition = startPoint.position;
        Vector3 endPointPosition = endPoint.position;

        float distance = Vector3.Distance(startPointPosition, endPointPosition);
        Vector3 midPoint = (startPointPosition + endPointPosition) / 2f;
        midPoint.y += parabolaHeight;

        for (int i = 0; i < pointCount; i++)
        {
            float t = (float)i / (float)(pointCount - 1);
            Vector3 pointOnParabola = CalculateParabolaPoint(startPointPosition, midPoint, endPointPosition, t);

            // Prefab'� spawn etmek i�in Instantiate kullan�yoruz.
            GameObject spawnedObject = Instantiate(prefab, pointOnParabola, Quaternion.identity);
            // Spawn edilen prefab'�n parent�n� ParabolaDrawer script'inin oldu�u obje yap�yoruz.
            spawnedObject.transform.SetParent(transform);
        }
    }

    Vector3 CalculateParabolaPoint(Vector3 start, Vector3 mid, Vector3 end, float t)
    {
        Vector3 result = Vector3.zero;

        float oneMinusT = 1f - t;

        result = (oneMinusT * oneMinusT * start) + (2f * oneMinusT * t * mid) + (t * t * end);

        return result;
    }
}
