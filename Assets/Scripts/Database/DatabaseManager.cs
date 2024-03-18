using Services;
using Services.Asset;
using Services.Event;
using SQLite4Unity3d;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DatabaseManager : NetworkBehaviour
{
    private IEventSystem eventSystem;
    private SQLiteConnection connection;
    private IAssetLoader assetLoader;

    private void Start()
    {
        DontDestroyOnLoad(this);
        eventSystem = ServiceLocator.Get<IEventSystem>();
        assetLoader = ServiceLocator.Get<IAssetLoader>();
    }

    public List<T> ExcuteQuery<T>(string name) where T : new ()
    {
        QuerySO so = assetLoader.Load<QuerySO>(name);
        return connection.Query<T>(so.content);
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        eventSystem.AddListener<StationInitializer>(EEvent.AfterChargingStationInitialized, AfterInitialized);
        if (IsServer)
        {
            try
            {
                string str = @"D:\Database\Platform.db";
                connection = new SQLiteConnection(str);
            }
            catch(Exception e)
            {
                Debug.LogException(e);
            }
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        eventSystem.RemoveListener<StationInitializer>(EEvent.AfterChargingStationInitialized, AfterInitialized);
        if (IsServer && connection != null)
        {
            connection.Close();
            connection.Dispose();
        }
    }

    private void AfterInitialized(StationInitializer initializer)
    {
        if(IsServer)
        {
            Dictionary<string, ChargingStation> temp = new Dictionary<string, ChargingStation>();
            for (int i = 0; i < initializer.stations.Length; i++)
            {
                temp.Add(initializer.stations[i].id, initializer.stations[i]);
            }
            List<ChargingStationData> datas = ExcuteQuery<ChargingStationData>("AllChargingStation");
            foreach (ChargingStation station in temp.Values)
            {
                ChargingStationData data = new ChargingStationData(station);
                connection.Insert(data);
            }
            Debug.Log(datas.Count);
        }
    }
}
