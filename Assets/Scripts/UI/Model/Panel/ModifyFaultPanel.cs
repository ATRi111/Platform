using TMPro;
using UnityEngine.UI;

public class ModifyFaultPanel : DataPanel
{
    private TMP_InputField tmp;
    private Toggle toggle;
    private StationDataManager dataManager;

    public FaultData activeFaultData;

    protected override void Awake()
    {
        base.Awake();
        tmp = GetComponentInChildren<TMP_InputField>();
        toggle = GetComponentInChildren<Toggle>();
        dataManager = DataManager.FindInstance<StationDataManager>();
    }

    public override void Refresh()
    {
        if(activeFaultData != null)
        {
            tmp.text = activeFaultData.Content;
            toggle.isOn = activeFaultData.Solved;
        }
        else
        {
            tmp.text = string.Empty;
            toggle.isOn = false;
        }
    }

    public void Confirm()
    {
        string content = tmp.text ?? string.Empty;
        if (activeFaultData == null)
            dataManager.InsertFault(activeStation.data.Id, content, toggle.isOn);
        else
            dataManager.ModifyFault(activeFaultData.Id, content, toggle.isOn);
        Hide();
    }

    public override void Hide()
    {
        base.Hide();
        activeFaultData = null;
    }
}
