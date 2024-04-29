using System.Collections.Generic;
using Tools;
using UnityEngine;

public class FaultRecordPanel : DataPanel
{
    private TableUIHelper tableUIHelper;
    [SerializeField]
    private ModifyFaultPanel panel;

    protected override void Awake()
    {
        base.Awake();
        tableUIHelper = GetComponentInChildren<TableUIHelper>();
    }

    private void Start()
    {
        tableUIHelper.Initialize(RowCount, RowContent);
    }

    protected override void Update()
    {
        base.Update();
        if(canvasGrounp.Visible && Input.GetMouseButtonDown(0) && !panel.Visible)
        {
            int index = tableUIHelper.ClickIndex();
            if(index >= 0 && index < activeStation.faultRecord.Count)
            {
                ModifyFault(activeStation.faultRecord[index]);
            }
        }
    }

    public void NewFault()
        => ModifyFault(null);

    public void ModifyFault(FaultData faultData)
    {
        panel.activeFaultData = faultData;
        panel.Show();
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
        if(data.Solved)
            ret.Add("已解决".ColorText("green"));
        else
            ret.Add("未解决".ColorText("red"));
        return ret;
    }

    public override void Refresh()
    {
        if (activeStation == null)
            return;
        tableUIHelper.Refresh();
    }
}
