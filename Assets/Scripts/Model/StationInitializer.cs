using Services;
using Services.Event;
using UnityEngine;

public class StationInitializer : MonoBehaviour
{
    private IEventSystem eventSystem;
    public ChargingStation[] stations;

    private void Awake()
    {
        eventSystem = ServiceLocator.Get<IEventSystem>();
        stations = GetComponentsInChildren<ChargingStation>();
        for (int i = 0; i < stations.Length; i++)
        {
            stations[i].id = i.ToString().PadLeft(6, '0');
            stations[i].data = new StationStaticData()
            {
                type = "落地式",
                voltage = "直流380V",
                power = 30f,
                price = 0.6f,
            };
        }
    }

    private void Start()
    {
        eventSystem.Invoke(EEvent.AfterChargingStationInitialized, this);
    }
}
