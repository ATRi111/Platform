using Services.Event;
using System.Collections.Generic;

public class StationDataManager : DataManager
{
    private Dictionary<string, ChargingStation> stationDict = new Dictionary<string, ChargingStation>();

    protected override void Awake()
    {
        ChargingStation[] stations = GetComponentsInChildren<ChargingStation>();
        for (int i = 0; i < stations.Length; i++)
        {
            if(i <= stations.Length / 2)
                stations[i].data = new ChargingStationData(i.ToString().PadLeft(6, '0'), "落地式", 0.5f, 150f, "直流380V");
            else
                stations[i].data = new ChargingStationData(i.ToString().PadLeft(6, '0'), "落地式", 0.6f, 300f, "直流380V");
            stationDict.Add(stations[i].data.Id, stations[i]);
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

    protected override void Start()
    {
        if (IsServer)
        {
            Initialize();
            UpdateData();
        }
    }

    private void Initialize()
    {
        List<ChargingStationData> result = databaseManager.Query<ChargingStationData>("AllChargingStation");
        HashSet<string> ids = new HashSet<string>();
        for (int i = 0; i < result.Count; i++)
        {
            ids.Add(result[i].Id);
        }
        foreach (ChargingStation station in stationDict.Values)
        {
            if(!ids.Contains(station.data.Id))
                databaseManager.Insert(station.data);
        }
    }

    protected override void UpdateData()
    {
        if(IsServer)
        {
            List<ChargingStationData> result = databaseManager.Query<ChargingStationData>("AllChargingStation");
            for (int i = 0; i < result.Count; i++)
            {
                if (stationDict.ContainsKey(result[i].Id))
                    stationDict[result[i].Id].data = result[i];
            }
        }
    }

    private void GetUsage()
    {

    }
}
