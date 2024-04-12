using Newtonsoft.Json;
using Services.Event;
using System;
using System.Collections.Generic;
using UnityEngine;

public class StationDataManager : DataManager
{
    public class SyncData
    {
        public List<ChargingStationData> stationDatas;
        public Dictionary<string, List<UsageData>> usageDatas;
        public Dictionary<string, List<FaultData>> faultDatas;

        public SyncData(List<ChargingStationData> stationDatas, Dictionary<string, List<UsageData>> usageDatas, Dictionary<string, List<FaultData>> faultDatas)
        {
            this.stationDatas = stationDatas;
            this.usageDatas = usageDatas;
            this.faultDatas = faultDatas;
        }
    }

    public Dictionary<string, ChargingStation> stationDict = new Dictionary<string, ChargingStation>();

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

    protected override object LocalQuery()
    {
        List<ChargingStationData> stationDatas = databaseManager.Query<ChargingStationData>("AllChargingStation");
        Dictionary<string,List<UsageData>> usageDatas = new Dictionary<string, List<UsageData>>();
        Dictionary<string,List<FaultData>> faultDatas = new Dictionary<string, List<FaultData>>();
        foreach (string id in stationDict.Keys)
        {
            usageDatas.Add(id, databaseManager.QueryWithArguments<UsageData>("NewUsage", id));
            faultDatas.Add(id, databaseManager.QueryWithArguments<FaultData>("NewFault", id));
        }
        return new SyncData(stationDatas, usageDatas, faultDatas);
    }   

    protected override void UpdateState()
    {
        base.UpdateState();
        SyncData wholeData = JsonConvert.DeserializeObject<SyncData>(dataJson);
        List<ChargingStationData> stationDatas = wholeData.stationDatas;
        for (int i = 0; i < stationDatas.Count; i++)
        {
            string id = stationDatas[i].Id;
            if (stationDict.ContainsKey(id))
                stationDict[id].data = stationDatas[i];
        }
        foreach (var station in stationDict)
        {
            station.Value.usageRecord = wholeData.usageDatas[station.Key];
            station.Value.faultRecord = wholeData.faultDatas[station.Key];
        }
        eventSystem.Invoke(EEvent.Refresh);
    }


    public void InsertUsage(int phoneNumber, string stationId, EStationState state)
    {
        string query = $"INSERT INTO Usage VALUES (NULL, {phoneNumber}, '{stationId}', '{DateTime.Now}',{(int)state})";
        RemoteQueryRpc(query, localClientId);
    }

    public void InsertFault(string stationId, string content,bool solved)
    {
        int flag = solved ? 1 : 0;
        string query = $"INSERT INTO Fault VALUES (NULL, '{stationId}',  '{DateTime.Now}','{content}',{flag})";
        RemoteQueryRpc(query, localClientId);
    }

    public void ModifyFault(int id, string content, bool solved)
    {
        string s = solved ? "1" : "0";
        string query = $"UPDATE Fault SET content='{content}', solved = {s} WHERE id = {id}";
        RemoteQueryRpc(query, localClientId);
    }
}
