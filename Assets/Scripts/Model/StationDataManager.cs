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
            stations[i].id = i.ToString().PadLeft(6, '0');
            stations[i].data = new StationStaticData()
            {
                type = "落地式",
                voltage = i<= stations.Length/2 ? "直流380V" : "交流220V",
                power = 30f,
                price = 0.6f,
            };
        }

    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        eventSystem.AddListener(EEvent.Refresh, UpdateData);
    }

    public override void OnNetworkDespawn()
    {
        eventSystem.RemoveListener(EEvent.Refresh, UpdateData);
        base.OnNetworkDespawn();
    }

    private void Start()
    {
        if (IsServer)
        {
            Initialize();
            UpdateData();
        }
    }

    private void Initialize()
    {
        Dictionary<string, ChargingStation> temp = new Dictionary<string, ChargingStation>();
        for (int i = 0; i < stations.Length; i++)
        {
            temp.Add(stations[i].id, stations[i]);
        }
        List<ChargingStationData> result = databaseManager.Query<ChargingStationData>("AllChargingStation");
        for (int i = 0; i < result.Count; i++)
        {
            temp.Remove(result[i].Id);
        }
        foreach (ChargingStation station in temp.Values)
        {
            databaseManager.Insert(new ChargingStationData(station));
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
