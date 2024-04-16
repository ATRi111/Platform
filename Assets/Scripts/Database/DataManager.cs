using Newtonsoft.Json;
using Services;
using Services.Event;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class DataManager : NetworkBehaviour
{
    public static T FindInstance<T>() where T : NetworkBehaviour
    {
        Type type = typeof(T);
        return GameObject.Find(type.Name).GetComponent<T>();
    }

    public static DateTime ToDateTime(string s)
    {
        string[] ss = s.Split(' ');
        string[] date = ss[0].Split('/');
        string[] time = ss[1].Split(':');
        List<int> temp = new List<int>();
        for (int i = 0; i < date.Length; i++)
        {
            temp.Add(int.Parse(date[i]));
        }
        for (int i = 0; i < time.Length; i++)
        {
            temp.Add(int.Parse(time[i]));
        }
        return new DateTime(temp[0], temp[1], temp[2], temp[3], temp[4], temp[5], DateTimeKind.Utc);
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
            eventSystem.AddListener<ChargingStation>(EEvent.SelectStation, OnOpenChargingStationPanel);
            AskForJsonRpc(localClientId);
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        if(!IsServer)
            eventSystem.RemoveListener<ChargingStation>(EEvent.SelectStation, OnOpenChargingStationPanel);
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
    [Rpc(SendTo.ClientsAndHost, AllowTargetOverride = true)]
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
