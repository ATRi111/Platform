using Newtonsoft.Json;
using Services;
using Services.Event;
using TMPro;
using UnityEngine;

public class ChargingStationPanel : MonoBehaviour
{
    private IEventSystem eventSystem;
    [SerializeField]
    private TextMeshProUGUI stationData;
    [SerializeField]
    private TextMeshProUGUI stationState;
    private MyCanvasGrounp canvasGrounp;
    private ChargingStation activeStation;

    private void Awake()
    {
        eventSystem = ServiceLocator.Get<IEventSystem>();
        canvasGrounp = GetComponent<MyCanvasGrounp>();
        eventSystem.AddListener<ChargingStation>(EEvent.OpenChargingStationPanel, Show);
        eventSystem.AddListener(EEvent.Refresh, ShowStationState);
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            Hide();
        }
    }

    private void OnDestroy()
    {
        eventSystem.RemoveListener<ChargingStation>(EEvent.OpenChargingStationPanel, Show);
        eventSystem.RemoveListener(EEvent.Refresh, ShowStationState);
    }

    private void Show(ChargingStation station)
    {
        activeStation = station;
        ShowStationData();
        ShowStationState();
        canvasGrounp.Visible = true;
    }

    public void ShowStationData()
    {
        if (activeStation == null)
            return;
        ChargingStationData data = activeStation.data;
        stationData.text = $"编号:{data.Id}\n" +
            $"充电桩类型:{data.Type}\n" +
            $"输入电压:{data.Voltage}\n" +
            $"输出功率:{data.Power:F1}kW\n" +
            $"电价:{data.Price:F2}元/度\n";
    }

    public void ShowStationState()
    {
        if (activeStation == null)
            return;
        stationState.text = $"当前状态:{activeStation.GetState()}\n" +
            $"充电进度:{activeStation.rate:p0}\n";
    }

    private void Hide()
    {
        canvasGrounp.Visible = false;
        activeStation = null;
    }
}