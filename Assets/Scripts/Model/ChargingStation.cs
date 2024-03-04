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

    public string GetCarType()
    {
        return null;
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