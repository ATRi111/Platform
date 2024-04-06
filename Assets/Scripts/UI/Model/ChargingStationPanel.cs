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
    [HideInInspector]
    public ChargingStation activeStation;

    private void Awake()
    {
        eventSystem = ServiceLocator.Get<IEventSystem>();
        canvasGrounp = GetComponent<MyCanvasGrounp>();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            Hide();
        }
    }

    private void OnEnable()
    {
        eventSystem.AddListener<ChargingStation>(EEvent.OpenChargingStationPanel, Show);
    }

    private void OnDisable()
    {
        eventSystem.RemoveListener<ChargingStation>(EEvent.OpenChargingStationPanel, Show);
    }

    private void Show(ChargingStation station)
    {
        activeStation = station;
        ShowStationData();
        ShowStationState();
        canvasGrounp.Visible = true;
    }

    private void ShowStationData()
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

    private void ShowStationState()
    {
        if (activeStation == null)
            return;
        stationState.text = $"当前状态:{activeStation.GetState()}" +
            $"充电进度:{activeStation.rate:p0}";
    }

    private void Hide()
    {
        canvasGrounp.Visible = false;
        activeStation = null;
    }
}