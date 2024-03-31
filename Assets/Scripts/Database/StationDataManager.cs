using Newtonsoft.Json;
using Services;
using System.Collections.Generic;
using UnityEngine;

public class StationDataManager : DataManager
{
    private class SyncData
    {
        public List<ChargingStationData> stationDatas;
        public List<UsageData> usageDatas;

        public SyncData(List<ChargingStationData> stationDatas, List<UsageData> usageDatas)
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
        if (IsServer)
            Initialize();
        base.OnNetworkSpawn();
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

    protected override void ReadData()
    {
        List<ChargingStationData> stationDatas = databaseManager.Query<ChargingStationData>("AllChargingStation");
        List<UsageData> usageDatas = databaseManager.Query<UsageData>("AllUsage");
        SyncData data = new SyncData(stationDatas, usageDatas);
        dataJson = JsonConvert.SerializeObject(data, JsonTool.DefaultSettings);
        SendJsonRpc(dataJson);
    }

    public override void UpdateData()
    {
        Debug.Log(dataJson);
        SyncData data = JsonConvert.DeserializeObject<SyncData>(dataJson);
        List<ChargingStationData> stationDatas = data.stationDatas;
        List<UsageData> usageDatas = data.usageDatas;
        for (int i = 0; i < stationDatas.Count; i++)
        {
            string id = stationDatas[i].Id;
            if (stationDict.ContainsKey(id))
                stationDict[id].data = stationDatas[i];
        }
        foreach (ChargingStation station in stationDict.Values)
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
