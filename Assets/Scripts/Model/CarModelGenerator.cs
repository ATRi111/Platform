using MyTimer;
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
    private Metronome metronome;

    private void Awake()
    {
        objectManager = ServiceLocator.Get<IObjectManager>(); 
        eventSystem = ServiceLocator.Get<IEventSystem>();
        station = GetComponentInParent<ChargingStation>();
        metronome = new Metronome();
        metronome.AfterCompelete += (float _) => AfterCarChange();
        metronome.Initialize(5f);
    }

    private void OnEnable()
    {
        eventSystem.AddListener(EEvent.AfterCarChange, AfterCarChange);
    }

    private void OnDisable()
    {
        eventSystem.AddListener(EEvent.AfterCarChange, AfterCarChange);
    }

    private void AfterCarChange()
    {
        EStationState state = station.GetStationState();
        switch(state)
        {
            case EStationState.Available:
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
