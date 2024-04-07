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
    protected string dataJson;
    protected ulong localClientId;
    protected DatabaseManager databaseManager;

    protected virtual void Awake()
    {
        eventSystem = ServiceLocator.Get<IEventSystem>();
        databaseManager = DatabaseManager.FindInstance();
        dataJson = string.Empty;
        localClientId = NetworkManager.Singleton.LocalClientId;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (IsServer)
        {
            ReadData();
        }
        else
        {
            eventSystem.AddListener<ChargingStation>(EEvent.OpenChargingStationPanel, OnOpenChargingStationPanel);
            AskForJsonRpc(localClientId);
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        if(!IsServer)
            eventSystem.RemoveListener<ChargingStation>(EEvent.OpenChargingStationPanel, OnOpenChargingStationPanel);
    }

    protected virtual void Start()
    {

    }

    /// <summary>
    /// (服务器)从数据库读数据,并更新json
    /// </summary>
    protected void ReadData()
    {
        dataJson = JsonConvert.SerializeObject(LocalQuery(), JsonTool.DefaultSettings);
        UpdateState();
    }

    protected abstract object LocalQuery();

    [Rpc(SendTo.Server)]
    protected void AskForJsonRpc(ulong clientId)
        => RemoteQueryRpc(null, clientId);

    /// <summary>
    /// 远程访问数据库必须经由此方法
    /// </summary>
    [Rpc(SendTo.Server)]
    protected void RemoteQueryRpc(string content, ulong clientId)
    {
        if(!string.IsNullOrEmpty(content))
            databaseManager.QueryWithoutSO<FaultData>(content);
        Debug.LogWarning($"send data to cilent {clientId}");
        ReadData();
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

    }

    protected void OnOpenChargingStationPanel(ChargingStation _)
        => AskForJsonRpc(localClientId);
}
