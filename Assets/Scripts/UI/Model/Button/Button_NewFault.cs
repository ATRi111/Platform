using UnityEngine;

public class Button_NewFault : Button_ChangingStation
{
    [SerializeField]
    private ModifyFaultPanel panel;

    protected override void Refresh()
    {
        if (activeStation == null)
            return;
        switch(activeStation.GetState())
        {
            case EStationState.Available:
            case EStationState.Repairing:
                gameObject.SetActive(true);
                break;
            default:
                gameObject.SetActive(false);
                break;
        }
    }
    protected override void OnClick()
    {
        panel.Show();
    }
}
