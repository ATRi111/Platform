using Services;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChargingStationTrigger : MyButton,IPointerEnterHandler,IPointerExitHandler
{
    private ChargingStation station;
    [SerializeField]
    private Vector3 offset;

    protected override void Awake()
    {
        base.Awake();
        station = GetComponentInParent<ChargingStation>();
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
    protected override void OnClick()
    {
        eventSystem.Invoke(EEvent.OpenChargingStationPanel, station);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + offset, 0.01f);
    }
}
