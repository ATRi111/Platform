using System.Collections.Generic;
using UnityEngine;

public class UseRecordPanel : DataPanel
{
    private TableUIHelper tableUIHelper;

    protected override void Awake()
    {
        base.Awake();
        tableUIHelper = GetComponentInChildren<TableUIHelper>();
    }

    private void Start()
    {
        tableUIHelper.Initialize(RowCount, RowContent);
    }

    public int RowCount()
    {
        if(activeStation == null)
            return 0;
        return activeStation.usageRecord.Count;
    }

    public List<string> RowContent(int index)
    {
        if (activeStation == null)
            return null;
        if(index < 0 || index >= activeStation.usageRecord.Count)
        {
            Debug.LogWarning(index);
            return null;
        }
        string content = string.Empty;
        UsageData data = activeStation.usageRecord[index];
        EStationState prev;

        if (index == activeStation.usageRecord.Count - 1)
            prev = EStationState.Available;
        else
            prev = (EStationState)activeStation.usageRecord[index + 1].State;

        switch ((EStationState)data.State)
        {
            case EStationState.Available:
                if (prev == EStationState.Occuppied)
                    content = $"用户{EmphasizedText(data.PhoneNumber)}于{EmphasizedText(data.Time)}使用完毕此充电桩";
                else if (prev == EStationState.Booked)
                    content = $"用户{EmphasizedText(data.PhoneNumber)}于{EmphasizedText(data.Time)}取消预订此充电桩";
                else
                    content = $"此充电桩于{EmphasizedText(data.Time)}变为空闲状态";
                break;
            case EStationState.Occuppied:
                if (prev == EStationState.Booked)
                    content = $"用户{EmphasizedText(data.PhoneNumber)}于{EmphasizedText(data.Time)}开始使用此充电桩";
                else
                    content = $"此充电桩于{EmphasizedText(data.Time)}变为被占用状态";
                break;
            case EStationState.Booked:
                if (prev == EStationState.Available)
                    content = $"用户{EmphasizedText(data.PhoneNumber)}于{EmphasizedText(data.Time)}预订此充电桩";
                else
                    content = $"此充电桩于{EmphasizedText(data.Time)}变为被预订状态";
                break;
        }
        return new List<string> { content };
    }

    public override void Refresh()
    {
        if (activeStation == null)
            return;

        string title = $"{EmphasizedText(activeStation.data.Id)}号充电桩的使用记录(从新到旧)";
        tableUIHelper.SetTitle(new List<string> { title });
        tableUIHelper.Refresh();
    }
}
