using Newtonsoft.Json;
using Services.Event;
using System.Collections.Generic;

public class UserDataManager : DataManager
{
    public class SyncData
    {
        public List<UserData> userDatas;

        public SyncData(List<UserData> userDatas)
        {
            this.userDatas = userDatas;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        localPhoneNumber = 123456;
    }

    public Dictionary<int,UserData> userDict = new Dictionary<int,UserData>();
    public int localPhoneNumber;

    protected override object LocalQuery()
    {
        List<UserData> result = databaseManager.Query<UserData>("AllUser");
        return new SyncData(result);
    }

    protected override void UpdateState()
    {
        base.UpdateState();
        SyncData data = JsonConvert.DeserializeObject<SyncData>(dataJson);
        userDict.Clear();
        for (int i = 0; i < data.userDatas.Count; i++)
        {
            userDict.Add(data.userDatas[i].PhoneNumber, data.userDatas[i]);
        }
        eventSystem.Invoke(EEvent.Refresh);
    }

    public void ModifyCarType(string carType)
    {
        string query = $"UPDATE User SET model='{carType}' WHERE phoneNumber = {localPhoneNumber}";
        RemoteQueryRpc(query, localClientId);
    }

    public void NewUser(int phoneNumber, string password)
    {
        string query = $"INSERT INTO User VALUES ({phoneNumber}, 'Sport',  '{password}')";
        RemoteQueryRpc(query, localClientId);
    }
}
