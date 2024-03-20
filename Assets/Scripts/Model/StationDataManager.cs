using Services;
using Services.Event;
using System.Collections.Generic;
using Unity.Netcode;

public class StationDataManager : NetworkBehaviour
{
    private IEventSystem eventSystem;
    private DatabaseManager databaseManager;
    public ChargingStation[] stations;
    public Dictionary<string, ChargingStationData> dataDict = new Dictionary<string, ChargingStationData>();

    private void Awake()
    {
        eventSystem = ServiceLocator.Get<IEventSystem>();
        if(NetworkManager.Singleton.IsServer)
            databaseManager = DatabaseManager.FindInstance();
        stations = GetComponentsInChildren<ChargingStation>();
        for (int i = 0; i < stations.Length; i++)
        {
            if(i <= stations.Length / 2)
                stations[i].data = new ChargingStationData(i.ToString().PadLeft(6, '0'), "落地式", 0.5f, 150f, "直流380V");
            else
                stations[i].data = new ChargingStationData(i.ToString().PadLeft(6, '0'), "落地式", 0.6f, 300f, "直流380V");
        }
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        eventSystem.AddListener(EEvent.Refresh, UpdateData);
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        eventSystem.RemoveListener(EEvent.Refresh, UpdateData);
    }

    private void Start()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            Initialize();
            UpdateData();
        }
    }

    private void Initialize()
    {
        Dictionary<string, ChargingStationData> temp = new Dictionary<string, ChargingStationData>();
        for (int i = 0; i < stations.Length; i++)
        {
            temp.Add(stations[i].data.Id, stations[i].data);
        }
        List<ChargingStationData> result = databaseManager.Query<ChargingStationData>("AllChargingStation");
        for (int i = 0; i < result.Count; i++)
        {
            temp.Remove(result[i].Id);
        }
        foreach (ChargingStationData data in temp.Values)
        {
            databaseManager.Insert(data);
        }
    }

    private void UpdateData()
    {
        dataDict.Clear();
        List<ChargingStationData> result = databaseManager.Query<ChargingStationData>("AllChargingStation");
        for (int i = 0; i < result.Count; i++)
        {
            dataDict.Add(result[i].Id, result[i]);
        }
    }
}
