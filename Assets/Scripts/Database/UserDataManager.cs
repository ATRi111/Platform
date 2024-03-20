using System.Collections.Generic;

public class UserDataManager : DataManager
{
    private bool dirty;
    private Dictionary<int,UserData> userDict = new Dictionary<int,UserData>();

    protected override void Awake()
    {
        base.Awake();
        dirty = true;
    }

    protected override void UpdateData()
    {
        if(IsServer && dirty)
        {
            userDict.Clear();
            List<UserData> result = databaseManager.Query<UserData>("AllUser");
            for (int i = 0; i < result.Count; i++)
            {
                userDict.Add(result[i].PhoneNumber, result[i]);
            }
            dirty = false;
        }
    }
}
