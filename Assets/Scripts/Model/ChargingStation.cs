using Unity.Collections;
using Unity.Netcode;

public class ChargingStation : NetworkBehaviour
{
    public NetworkVariable<StationStaticData> data;

    public void Update()
    {
        if (IsServer)
        {
            
        }
    }
}

public struct StationStaticData
{
    public FixedString32Bytes id;
    public FixedString32Bytes type;
    public float voltage;
    public float power;
    public float price;
}