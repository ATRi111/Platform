using Unity.Netcode;
using UnityEngine;

public class NetworkTest : MonoBehaviour
{
    private void Start()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnConnect;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnDisConnent;
        NetworkManager.Singleton.OnServerStarted += OnStart;
    }

    private void OnStart()
    {
        Debug.Log("server started");
    }

    private void OnConnect(ulong id)
    {
        Debug.Log($"client {id} conncted");
    }

    private void OnDisConnent(ulong id)
    {
        Debug.Log($"client {id} disconncted");
    }
}
