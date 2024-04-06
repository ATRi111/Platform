using Newtonsoft.Json;
using Services;
using Services.Event;
using System;
using Unity.Netcode;
using UnityEngine;

public abstract class DataManager : NetworkBehaviour
{
    public static T FindInstance<T>() where T : NetworkBehaviour
    {
        Type type = typeof(T);
        return GameObject.Find(type.Name).GetComponent<T>();
    }

    protected new static bool IsServer => NetworkManager.Singleton.IsServer;
    protected IEventSystem eventSystem;
    protected DatabaseManager databaseManager;
    protected string dataJson;

    protected virtual void Awake()
    {
        eventSystem = ServiceLocator.Get<IEventSystem>();
        databaseManager = DatabaseManager.FindInstance();
        dataJson = string.Empty;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsServer)
        {
            eventSystem.AddListener(EEvent.AfterDatabaseUpdate, ReadData);
            ReadData();
        }
        else
        {
            eventSystem.AddListener<ChargingStation>(EEvent.OpenChargingStationPanel, OnOpenChargingStationPanel);
            AskForJsonRpc(NetworkManager.LocalClientId);
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        if(IsServer)
        {
            eventSystem.RemoveListener(EEvent.AfterDatabaseUpdate, ReadData);
        }
        else
        {
            eventSystem.RemoveListener<ChargingStation>(EEvent.OpenChargingStationPanel, OnOpenChargingStationPanel);
        }
    }

    protected virtual void Start()
    {

    }

    /// <summary>
    /// (服务器)从数据库读数据,并更新json
    /// </summary>
    protected void ReadData()
    {
        dataJson = JsonConvert.SerializeObject(Query(), JsonTool.DefaultSettings);
        SendJsonRpc(dataJson);
        UpdateState();
    }

    protected abstract object Query();
  
    [Rpc(SendTo.Server)]
    protected void AskForJsonRpc(ulong clientId)
    {
        Debug.Log($"client {clientId} ask for json");
        SendJsonRpc(dataJson, RpcTarget.Single(clientId, RpcTargetUse.Temp));
    }
    /// <summary>
    /// 向客户端发送json
    /// </summary>
    [Rpc(SendTo.NotServer, AllowTargetOverride = true)]
    protected void SendJsonRpc(string json, RpcParams _ = default)
    {
        dataJson = json;
        UpdateState();
    }
    /// <summary>
    /// 根据json更新游戏对象的状态
    /// </summary>
    protected virtual void UpdateState()
    {
        Debug.Log(dataJson);
    }

    protected void OnOpenChargingStationPanel(ChargingStation _)
        => AskForJsonRpc(NetworkManager.LocalClientId);
}
