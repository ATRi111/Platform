using System.Collections.Generic;
using UnityEngine;

public class ChargingStation : MonoBehaviour
{
    public float rate;
    public ChargingStationData data;
    public List<UsageData> usageRecord = new List<UsageData>();

    public EStationState GetState()
    {
        if(usageRecord.Count == 0)
            return EStationState.Available;
        return (EStationState)usageRecord[^1].State;
    }

    public string GetCarType()
    {
        if(GetState() == EStationState.Ocuppied)
        {
            //
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