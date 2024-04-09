using System.Collections.Generic;
using UnityEngine;

public class FaultRecordPanel : DataPanel
{
    private TableUIHelper tableUIHelper;

    protected override void Awake()
    {
        base.Awake();
        tableUIHelper = GetComponentInChildren<TableUIHelper>();
    }

    private void Start()
    {
        tableUIHelper.Initialize(RowCount, RowContent, 10);
    }

    public int RowCount()
    {
        if(activeStation == null)
            return 0;
        return activeStation.faultRecord.Count;
    }

    public List<string> RowContent(int index)
    {
        if (activeStation == null)
            return null;
        if(index < 0 || index >= activeStation.faultRecord.Count)
        {
            Debug.LogWarning(index);
            return null;
        }
        List<string> ret = new List<string>();
        FaultData data = activeStation.faultRecord[index];
        ret.Add(data.Id.ToString());
        ret.Add(data.Time.ToString());
        ret.Add(data.Solved ? "已解决" : "未解决");
        return ret;
    }

    public override void Refresh()
    {
        if (activeStation == null)
            return;
        tableUIHelper.Refresh();
    }
}
