using System.Text;
using TMPro;
using UnityEngine;

public class ChargingStationPanel : DataPanel
{
    [SerializeField]
    private TextMeshProUGUI stationData;
    [SerializeField]
    private TextMeshProUGUI userMessage;
    private ChargingProgress progress;

    protected override void Awake()
    {
        base.Awake();
        progress = GetComponentInChildren<ChargingProgress>();
    }

    protected override void AfterSelectStation(ChargingStation station)
    {
        base.AfterSelectStation(station);
        Show();
    }

    public override void Show()
    {
        base.Show();
        ChargingStationData data = activeStation.data;
        stationData.text = $"编号:{data.Id}\n" +
            $"类型:{data.Type}\n" +
            $"输入电压:{data.Voltage}\n" +
            $"输出功率:{data.Power:F1}kW\n" +
            $"电价:{data.Price:F2}元/度\n";
    }

    public override void Refresh()
    {
        if (activeStation == null)
            return;
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"当前状态:{UsageData.StateName(activeStation.GetState())}");
        switch(activeStation.GetState())
        {
            case EStationState.Available:
            case EStationState.Repairing:
                break;
            case EStationState.Booked:
                if (IsServer)
                    sb.AppendLine($"手机号:{activeStation.usageRecord[0].PhoneNumber}");
                else if (activeStation.OccupiedOrBookedByLocalUser())
                    sb.AppendLine("您已预订");
                break;
            case EStationState.Occuppied:
                if (IsServer)
                    sb.AppendLine($"手机号:{activeStation.usageRecord[0].PhoneNumber}\n" +
                        $"车型:{activeStation.GetCarType()}");
                else if (activeStation.OccupiedOrBookedByLocalUser())
                    sb.AppendLine("您正在使用");
                break;
        }
        userMessage.text = sb.ToString();
        
    }

    protected override void Update()
    {
        base.Update();
        if (activeStation == null)
            return;
        if (activeStation.GetState() == EStationState.Occuppied)
        {
            progress.gameObject.SetActive(true);
            progress.SetRate(activeStation.GetRate());
        }
        else
        {
            progress.gameObject.SetActive(false);
        }
    }
}