using Unity.Netcode;
using UnityEngine;

public class ChargingStation : NetworkBehaviour
{
    public string id;
    public float rate;
    public StationStaticData data;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if(IsServer)
        {
        }
    }

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
    public string type;
    public string voltage;
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