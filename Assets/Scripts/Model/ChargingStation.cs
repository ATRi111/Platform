using Unity.Netcode;
using UnityEngine;

public class ChargingStation : NetworkBehaviour
{
    public float rate;
    public ChargingStationData data;

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
}

public enum EStationState
{
    Available,
    Booked,
    Ocuppied,
    Repairing,
}