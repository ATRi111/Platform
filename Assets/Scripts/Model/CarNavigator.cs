using UnityEngine;

public class CarNavigator : MonoBehaviour
{
    public Transform destination;

    private float speed = 1f;

    private void FixedUpdate()
    {
        float d = Time.fixedDeltaTime * speed;
        Vector3 v = destination.position - transform.position;
        if (v.magnitude < d)
        {
            transform.SetPositionAndRotation(destination.position, transform.parent.rotation);
            Destroy(this);
        }
        else
            transform.position += d * v.normalized;
    }
}
