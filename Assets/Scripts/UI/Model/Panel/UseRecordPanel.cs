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
        List<string> ret = new List<string>();
        UsageData data = activeStation.usageRecord[index];
        ret.Add(data.Id.ToString());
        ret.Add(data.StationId.ToString());
        ret.Add(data.Time.ToString());
        ret.Add(UsageData.StateName((EStationState)data.State));
        return ret;
    }

    public override void Refresh()
    {
        if (activeStation == null)
            return;
        tableUIHelper.Refresh();
    }
}
