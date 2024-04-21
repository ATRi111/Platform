using Services;
using Services.Event;
using Services.ObjectPools;
using System;
using UnityEngine;

public class CarModelGenerator : MonoBehaviour
{
    private IObjectManager objectManager;
    private IEventSystem eventSystem;
    private ChargingStation station;
    private Transform enterPoint;

    private EStationState prev;

    private void Awake()
    {
        prev = (EStationState)Enum.GetValues(typeof(EStationState)).Length;
        objectManager = ServiceLocator.Get<IObjectManager>(); 
        eventSystem = ServiceLocator.Get<IEventSystem>();
        station = GetComponentInParent<ChargingStation>();
        enterPoint = GameObject.Find("EnterPoint").transform;
    }

    private void OnEnable()
    {
        eventSystem.AddListener(EEvent.Refresh, Refresh);
    }

    private void OnDisable()
    {
        eventSystem.AddListener(EEvent.Refresh, Refresh);
    }

    private void Refresh()
    {
        EStationState state = station.GetState();
        if(prev != state)
        {
            ObjectPoolUtility.RecycleMyObjects(gameObject);
            switch (state)
            {
                case EStationState.Ocuppied:
                    GenerateCar(station.GetCarType());
                    break;
                default:
                    break;
            }
            prev = state;
        }
    }

    private void GenerateCar(string carType)
    {
        if (string.IsNullOrEmpty(carType))
            return;
        IMyObject obj = objectManager.Activate(carType, enterPoint.position, Vector3.zero, transform);
        obj.Transform.localScale = Vector3.one;
        CarNavigator navigator = obj.Transform.gameObject.AddComponent<CarNavigator>();
        navigator.Laucnch(transform.position, 2f + 0.2f * int.Parse(station.data.Id));
    }
}
