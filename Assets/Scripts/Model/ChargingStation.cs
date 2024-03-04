using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class ChargingStation : NetworkBehaviour
{
    public NetworkVariable<StationStaticData> data;

    public void Update()
    {
        if (IsServer)
        {
            
        }
    }

    public EStationState GetState()
    {
        return EStationState.Available;
    }

    public string GetCarType()
    {
        int r = Random.Range(0, 4);
        return r switch
        {
            0 => "Sedan",
            1 => "Jeep",
            2 => "Sport",
            _ => null
        };
    }

    private void OnValueChanged(StationStaticData prev, StationStaticData data)
    {

    }
}

[System.Serializable]
public struct StationStaticData
{
    public FixedString32Bytes id;
    public FixedString32Bytes type;
    public float voltage;
    public float power;
    public float price;
}

public enum EStationState
{
    Available,
    Booked,
    Ocuppied,
    Repairing,
}