using MySql.Data.MySqlClient;
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
    private MySqlConnection connection;

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
                string str = "server=127.0.0.1;" +
                    "port=3306;" +
                    "user=root;" +
                    "password=123456;" +
                    "database=data";
                connection = new MySqlConnection(str);
                connection.Open();
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
            using MySqlCommand command = connection.CreateCommand();
            command.CommandText = "all_chargingstation";
            command.CommandType = CommandType.StoredProcedure;
            using MySqlDataReader reader = command.ExecuteReader();
            for (int i = 0; reader.Read(); i++)
            {
                Debug.Log(reader[i]);
            }
        }
    }
}
