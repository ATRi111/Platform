using System.Collections.Generic;
using UnityEngine;

public class NavigationRoute : MonoBehaviour
{
    public List<Vector3> points = new List<Vector3>();

    private void Awake()
    {
        for (int i = 0; i < transform.childCount; i++) 
        {
            points.Add(transform.GetChild(i).position);
        }
    }

    public bool MoveTowards(int index, Transform car, float d)
    {
        Vector3 v = points[index] - car.position;
        if (v.magnitude > d)
        {
            car.position += d * v.normalized;
            return false;
        }
        car.position = points[index];
        return true;
    }

    public int TurnNext(int index, Transform car)
    {
        Vector3 v = points[index + 1] - points[index];
        car.transform.rotation = Quaternion.LookRotation(v);
        return index + 1;
    }
}
