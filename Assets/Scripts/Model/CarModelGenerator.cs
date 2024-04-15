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
    private EStationState init;

    private void Awake()
    {
        init = (EStationState)Enum.GetValues(typeof(EStationState)).Length;
        prev = init;
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
        if(prev == init)
        {
            IMyObject obj = objectManager.Activate(carType, transform.position, transform.eulerAngles, transform);
            obj.Transform.localScale = Vector3.one;
        }
        else
        {
            IMyObject obj = objectManager.Activate(carType, enterPoint.position, enterPoint.eulerAngles, transform);
            obj.Transform.localScale = Vector3.one;
            CarNavigator navigator = obj.Transform.gameObject.AddComponent<CarNavigator>();
            navigator.destination = transform;
        }
    }
}
