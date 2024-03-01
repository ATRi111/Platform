using Services;
using Services.Event;
using Services.ObjectPools;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class ChargingStation : NetworkBehaviour
{
    private IObjectManager objectManager;
    private IEventSystem eventSystem;
    public NetworkVariable<StationStaticData> data;

    private GameObject car;

    private void Awake()
    {
        objectManager = ServiceLocator.Get<IObjectManager>(); 
        eventSystem = ServiceLocator.Get<IEventSystem>();
    }

    public void Update()
    {
        if (IsServer)
        {
            
        }
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