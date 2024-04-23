using Unity.Netcode;
using UnityEngine;

public class LogPanel : MonoBehaviour
{
    private void Awake()
    {
        if(NetworkManager.Singleton.IsServer)
            Destroy(gameObject);
    }
}
