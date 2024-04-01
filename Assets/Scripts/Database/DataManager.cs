using Services;
using Services.Event;
using Unity.Netcode;
using UnityEngine;

public abstract class DataManager : NetworkBehaviour
{
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
            eventSystem.AddListener(EEvent.UpdateData, ReadData);
            ReadData();
        }
        else
        {
            AskForJsonRpc();
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        if(IsServer)
            eventSystem.RemoveListener(EEvent.UpdateData, ReadData);
    }

    protected virtual void Start()
    {

    }

    /// <summary>
    /// (服务器)从数据库读数据,并更新json
    /// </summary>
    protected abstract void ReadData();
  
    [Rpc(SendTo.Server)]
    protected void AskForJsonRpc()
    {
        SendJsonRpc(dataJson);
    }
    /// <summary>
    /// 向客户端发送json
    /// </summary>
    [Rpc(SendTo.NotServer)]
    protected void SendJsonRpc(string json)
    {
        dataJson = json;
        UpdateData();
    }
    /// <summary>
    /// 根据json更新数据
    /// </summary>
    protected virtual void UpdateData()
    {
        Debug.Log(dataJson);
    }
}
