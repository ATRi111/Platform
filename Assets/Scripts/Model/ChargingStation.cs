using System;
using System.Collections.Generic;
using UnityEngine;

public class ChargingStation : MonoBehaviour
{
    public ChargingStationData data;
    public List<UsageData> usageRecord = new List<UsageData>();
    public List<FaultData> faultRecord = new List<FaultData>();
    private UserDataManager userDataManager;
    private StationDataManager stationDataManager;

    [SerializeField]
    private float charingSpeed;
    private DateTime prevTime;

    private void Awake()
    {
        userDataManager = DataManager.FindInstance<UserDataManager>();
        stationDataManager = DataManager.FindInstance<StationDataManager>();
        prevTime = DateTime.Now;
    }

    public bool OccupiedOrBookedByLocalUser()
    {
        return userDataManager.localPhoneNumber == GetUserPhoneNumber();
    }

    public string GetUserPhoneNumber()
    {
        return GetState() switch
        {
            EStationState.Ocuppied or EStationState.Booked => usageRecord[0].PhoneNumber,
            _ => string.Empty,
        };
    }

    public float GetRate()
    {
        if (GetState() != EStationState.Ocuppied)
            return 0f;
        DateTime latest = DataManager.ToDateTime(usageRecord[0].Time);
        if(latest > prevTime)
        {
            prevTime = latest;
        }
        TimeSpan span = DateTime.Now - prevTime;
        float temp = (float)span.TotalMinutes * charingSpeed;
        return Mathf.Clamp01(temp);
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
            string phoneNumber = usageRecord[0].PhoneNumber;
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