using Services;
using Services.Event;
using System;
using Unity.Netcode;
using UnityEngine;

public class NetworkService : Service,IService
{
    public override Type RegisterType => typeof(NetworkService);
    [AutoService]
    protected IEventSystem eventSystem;

    protected override void Start()
    {
        base.Start();
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
