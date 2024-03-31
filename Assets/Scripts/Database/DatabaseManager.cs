using Services;
using Services.Asset;
using Services.Event;
using SQLite4Unity3d;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DatabaseManager : NetworkBehaviour
{
    public static DatabaseManager FindInstance()
        => GameObject.Find(nameof(DatabaseManager)).GetComponent<DatabaseManager>();

    private IEventSystem eventSystem;
    private SQLiteConnection connection;
    private IAssetLoader assetLoader;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        eventSystem = ServiceLocator.Get<IEventSystem>();
        assetLoader = ServiceLocator.Get<IAssetLoader>();
    }

    public List<T> Query<T>(string name) where T : new ()
    {
        QuerySO so = assetLoader.Load<QuerySO>(name);
        return connection.Query<T>(so.content);
    }

    public List<T> QueryWithArguments<T>(string name,params string[] args) where T : new()
    {
        QuerySO so = assetLoader.Load<QuerySO>(name);
        return connection.Query<T>(so.ReplaceArguments(args));
    }

    public int Insert(IEnumerable collection)
        => connection.InsertAll(collection);
    public int Insert<T>(T tuple)
        => connection.Insert(tuple);

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
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
        if (IsServer && connection != null)
        {
            connection.Close();
            connection.Dispose();
        }
    }
}
