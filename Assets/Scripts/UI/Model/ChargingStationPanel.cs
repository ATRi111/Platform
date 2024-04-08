using Services.Event;
using TMPro;
using UnityEngine;

public class ChargingStationPanel : DataPanel
{
    [SerializeField]
    private TextMeshProUGUI stationData;
    [SerializeField]
    private TextMeshProUGUI stationState;

    protected override void Awake()
    {
        base.Awake();
        eventSystem.AddListener<ChargingStation>(EEvent.OpenChargingStationPanel, Show);
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        eventSystem.RemoveListener<ChargingStation>(EEvent.OpenChargingStationPanel, Show);
    }

    public override void Show(ChargingStation station)
    {
        base.Show(station);
        ShowStationData();
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

    public override void Refresh()
    {
        if (activeStation == null)
            return;
        stationState.text = $"当前状态:{UsageData.StateName(activeStation.GetState())}\n" +
            $"充电进度:{activeStation.rate:p0}\n";
    }
}