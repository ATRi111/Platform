using Services;
using Services.Event;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChargingStationTriggerPanel : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private IEventSystem eventSystem;
    private ChargingStation station;
    [SerializeField]
    private Vector3 offset;

    private void Awake()
    {
        eventSystem = ServiceLocator.Get<IEventSystem>();
        station = GetComponentInParent<ChargingStation>();
        Image image = GetComponent<Image>();
        image.color = Color.clear;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        eventSystem.Invoke(EEvent.ShowChargingStaionMessage, station, transform.position + offset);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        eventSystem.Invoke(EEvent.HideChargingStaionMessage);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + offset, 0.01f);
    }
}
