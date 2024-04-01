using Services;
using Services.Event;
using Services.ObjectPools;
using UnityEngine;

public class CarModelGenerator : MonoBehaviour
{
    private IObjectManager objectManager;
    private IEventSystem eventSystem;
    private ChargingStation station;

    [SerializeField]
    private string carType;

    private void Awake()
    {
        objectManager = ServiceLocator.Get<IObjectManager>(); 
        eventSystem = ServiceLocator.Get<IEventSystem>();
        station = GetComponentInParent<ChargingStation>();
    }

    private void OnEnable()
    {
        eventSystem.AddListener(EEvent.Refresh, AfterRefresh);
    }

    private void OnDisable()
    {
        eventSystem.AddListener(EEvent.Refresh, AfterRefresh);
    }

    private void AfterRefresh()
    {
        EStationState state = station.GetState();
        switch(state)
        {
            case EStationState.Ocuppied:
                string newType = station.GetCarType();
                if (newType != carType)
                {
                    carType = newType;
                    ObjectPoolUtility.RecycleMyObjects(gameObject);
                    if (!string.IsNullOrEmpty(carType))
                    {
                        IMyObject obj = objectManager.Activate(carType, transform.position, transform.eulerAngles, transform);
                        obj.Transform.localScale = Vector3.one;
                    }
                }
                break;
            default:
                ObjectPoolUtility.RecycleMyObjects(gameObject);
                carType = null;
                break;
        }
    }
}
