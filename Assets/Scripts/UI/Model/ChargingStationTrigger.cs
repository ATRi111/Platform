using Services;
using Services.Event;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChargingStationTrigger : MonoBehaviour ,IPointerEnterHandler,IPointerExitHandler
{
    private ChargingStation station;
    [SerializeField]
    private Vector3 offset;
    private IEventSystem eventSystem;

    private void Awake()
    {
        station = GetComponentInParent<ChargingStation>();
        eventSystem = ServiceLocator.Get<IEventSystem>();
        Image image = GetComponent<Image>();
        image.color = Color.clear;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventSystem.Invoke(EEvent.Refresh);
        eventSystem.Invoke(EEvent.ShowChargingStaionMessageWindow, station, transform.position + offset);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventSystem.Invoke(EEvent.HideChargingStaionMessageWindow);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + offset, 0.01f);
    }
}
