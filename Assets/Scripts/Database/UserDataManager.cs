using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class UserDataManager : DataManager
{
    private class SyncData
    {
        public List<UserData> userDatas;

        public SyncData(List<UserData> userDatas)
        {
            this.userDatas = userDatas;
        }
    }

    private Dictionary<int,UserData> userDict = new Dictionary<int,UserData>();

    protected override void ReadData()
    {
        List<UserData> result = databaseManager.Query<UserData>("AllUser");
        SyncData data = new SyncData(result);
        dataJson = JsonConvert.SerializeObject(data);
        SendJsonRpc(dataJson);
    }

    protected override void UpdateData()
    {
        base.UpdateData();
        SyncData data = JsonConvert.DeserializeObject<SyncData>(dataJson);
        userDict.Clear();
        for (int i = 0; i < data.userDatas.Count; i++)
        {
            userDict.Add(data.userDatas[i].PhoneNumber, data.userDatas[i]);
        }
    }
}
