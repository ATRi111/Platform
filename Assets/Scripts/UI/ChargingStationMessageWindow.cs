using Services;
using Services.Event;
using System.Text;
using TMPro;
using UnityEngine;

public class ChargingStationMessageWindow : MonoBehaviour
{
    private IEventSystem eventSystem;
    private TextMeshProUGUI tmp;
    private MyCanvasGrounp canvasGrounp;

    private void Awake()
    {
        eventSystem = ServiceLocator.Get<IEventSystem>();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        canvasGrounp = GetComponent<MyCanvasGrounp>();
    }

    private void OnEnable()
    {
        eventSystem.AddListener<ChargingStation, Vector3>(EEvent.ShowChargingStaionMessageWindow, Show);
        eventSystem.AddListener(EEvent.HideChargingStaionMessageWindow, Hide);
    }

    private void OnDisable()  
    {
        eventSystem.RemoveListener<ChargingStation, Vector3>(EEvent.ShowChargingStaionMessageWindow, Show);
        eventSystem.RemoveListener(EEvent.HideChargingStaionMessageWindow, Hide);
    }

    private void Show(ChargingStation station, Vector3 position)
    {
        transform.position = position;
        StringBuilder sb = new StringBuilder();
        sb.AppendLine(station.data.Value.id.ToString());
        string s = station.GetState() switch
        {
            EStationState.Available => "空闲",
            EStationState.Booked => "被预定",
            EStationState.Ocuppied => "使用中",
            EStationState.Repairing => "维修中",
            _ => string.Empty,
        };
        sb.AppendLine(s);
        sb.AppendLine("点击查看更多信息");
        tmp.text = sb.ToString();
        canvasGrounp.Visible = true;
    }

    private void Hide()
    {
        canvasGrounp.Visible = false;
    }
}