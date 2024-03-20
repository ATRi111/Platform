using System.Collections;
using Unity.Netcode;
using UnityEngine;

public class AutoSpawn : MonoBehaviour
{
    private NetworkObject parent;
    private NetworkObject self;

    private void Awake()
    {
        if(!NetworkManager.Singleton.IsServer)
        {
            Destroy(this);
            return;
        }

        self = GetComponent<NetworkObject>();
        if (transform.parent != null)
            parent = transform.parent.GetComponentInParent<NetworkObject>();
        else
            parent = null;
    }

    private void Start()
    {
        if (parent == null)
            self.Spawn();
        else
            StartCoroutine(DelaySpawn());
    }

    private IEnumerator DelaySpawn()
    {
        yield return new WaitForEndOfFrame();
        self.Spawn();
        transform.SetParent(parent.transform);
    }
}
