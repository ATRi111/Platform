using Newtonsoft.Json;
using System.Collections.Generic;

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

    public Dictionary<int,UserData> userDict = new Dictionary<int,UserData>();

    protected override object Query()
    {
        List<UserData> result = databaseManager.Query<UserData>("AllUser");
        return new SyncData(result);
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
