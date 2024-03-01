using Services;
using Services.Event;
using TMPro;
using UnityEngine;

public class ChargingStationMessagePanel : MonoBehaviour
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
        eventSystem.AddListener<ChargingStation, Vector3>(EEvent.ShowChargingStaionMessage, Show);
        eventSystem.AddListener(EEvent.HideChargingStaionMessage, Hide);
    }

    private void OnDisable()
    {
        eventSystem.RemoveListener<ChargingStation, Vector3>(EEvent.ShowChargingStaionMessage, Show);
        eventSystem.RemoveListener(EEvent.HideChargingStaionMessage, Hide);
    }

    private void Show(ChargingStation station, Vector3 position)
    {
        transform.position = position;
        StationStaticData data = station.data.Value;
        tmp.text = $"编号:{data.id}\n" +
            $"充电桩类型:{data.type}\n" +
            $"输入电压:{data.voltage:F1}V\n" +
            $"输出功率:{data.power:F1}kW\n" +
            $"电价:{data.price:F2}￥/kWh\n";
        canvasGrounp.Visible = true;
    }

    private void Hide()
    {
        canvasGrounp.Visible = false;
    }
}