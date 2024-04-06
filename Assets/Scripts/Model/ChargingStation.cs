using System.Collections.Generic;
using UnityEngine;

public class ChargingStation : MonoBehaviour
{
    public float rate;
    public ChargingStationData data;
    public List<UsageData> usageRecord = new List<UsageData>();
    public List<FaultData> faultRecord = new List<FaultData>();
    private UserDataManager userDataManager;
    private StationDataManager stationDataManager;

    private void Awake()
    {
        userDataManager = DataManager.FindInstance<UserDataManager>();
    }

    public EStationState GetState()
    {
        for (int i = 0; i < faultRecord.Count; i++)
        {
            if (faultRecord[i].Solved == false)
                return EStationState.Repairing;
        }
        if(usageRecord.Count == 0)
            return EStationState.Available;
        return (EStationState)usageRecord[0].State;
    }

    public string GetCarType()
    {
        if(GetState() == EStationState.Ocuppied)
        {
            int phoneNumber = usageRecord[0].PhoneNumber;
            if(userDataManager.userDict.ContainsKey(phoneNumber))
                return userDataManager.userDict[phoneNumber].Model;
        }
        return null;
    }
}

public enum EStationState
{
    Available,
    Booked,
    Ocuppied,
    Repairing,
}