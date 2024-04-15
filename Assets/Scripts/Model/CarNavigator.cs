using Tools;
using UnityEngine;

public class CarNavigator : MonoBehaviour
{
    public Vector3 destination;

    private NavigationRoute route;
    [SerializeField]
    private int pointIndex;
    private bool align;
    private readonly float alignDistance = 0.02f;
    private readonly float closeDistance = 2f;
    private readonly float speed = 2f;

    private void Awake()
    {
        route = GameObject.Find("NavigationRoute").GetComponent<NavigationRoute>();
        pointIndex = 0;
    }

    private void FixedUpdate()
    {
        float d = Time.fixedDeltaTime * speed;
        if (!align)
        {
            if(Mathf.Abs(transform.position.z - destination.z) < alignDistance && (destination - transform.position).magnitude < closeDistance)
            {
                transform.localRotation = Quaternion.identity;
                transform.position.ResetZ(destination.z);
                align = true;
            }
        }
        if (align)
        {
            Vector3 v = destination - transform.position;
            if (v.magnitude > d)
            {
                transform.position += d * v.normalized;
            }
            else
            {
                transform.position = destination;
                Destroy(this);
            }
        }
        else
        {
            bool reach = route.MoveTowards(pointIndex, transform, d);
            if (reach)
                pointIndex = route.TurnNext(pointIndex, transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(2f * closeDistance, 1f, 2 * alignDistance));
    }
}
