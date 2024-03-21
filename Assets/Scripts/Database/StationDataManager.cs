using Newtonsoft.Json;
using Services;
using Services.Event;
using System.Collections.Generic;
using Unity.Netcode;

public class StationDataManager : DataManager
{
    private class RpcData
    {
        public List<ChargingStationData> stationDatas;
        public List<UsageData> usageDatas;

        public RpcData(List<ChargingStationData> stationDatas, List<UsageData> usageDatas)
        {
            this.stationDatas = stationDatas;
            this.usageDatas = usageDatas;
        }
    }

    private Dictionary<string, ChargingStation> stationDict = new Dictionary<string, ChargingStation>();

    protected override void Awake()
    {
        base.Awake();
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
        eventSystem.AddListener(EEvent.Refresh, GetDataRpc); 
        if (IsServer)
        {
            Initialize();
            GetDataRpc();
        }
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        eventSystem.RemoveListener(EEvent.Refresh, GetDataRpc);
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

    [Rpc(SendTo.Server)]
    protected override void GetDataRpc()
    {
        List<ChargingStationData> stationDatas = databaseManager.Query<ChargingStationData>("AllChargingStation");
        List<UsageData> usageDatas = databaseManager.Query<UsageData>("AllUsage");
        RpcData data = new RpcData(stationDatas, usageDatas);
        string json = JsonConvert.SerializeObject(data, JsonTool.DefaultSettings);
        SendDataRpc(json);
    }

    [Rpc(SendTo.ClientsAndHost)]
    private void SendDataRpc(string json)
    {
        RpcData data = JsonConvert.DeserializeObject<RpcData>(json);
        List<ChargingStationData> stationDatas = data.stationDatas;
        List<UsageData> usageDatas = data.usageDatas;
        for (int i = 0; i < stationDatas.Count; i++)
        {
            string id = stationDatas[i].Id;
            if (stationDict.ContainsKey(id))
                stationDict[id].data = stationDatas[i];
        }
        foreach(ChargingStation station in stationDict.Values)
        {
            station.usageRecord.Clear();
        }
        for (int i = 0; i < usageDatas.Count; i++)
        {
            string id = usageDatas[i].StationId;
            if (stationDict.ContainsKey(id))
                stationDict[id].usageRecord.Add(usageDatas[i]);
        }
    }
}
