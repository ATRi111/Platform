using SQLite4Unity3d;
using Services;
using Services.Event;
using System;
using System.Collections.Generic;
using System.Data;
using Unity.Netcode;
using UnityEngine;

public class DatabaseManager : NetworkBehaviour
{
    private IEventSystem eventSystem;
    private SQLiteConnection connection;

    private void Start()
    {
        eventSystem = ServiceLocator.Get<IEventSystem>();
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
        }
    }
}
