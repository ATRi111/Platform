using Newtonsoft.Json;
using UnityEngine;

public class Button_Use : Button_ChangingStation
{
    protected override void Refresh()
    {
        if (activeStation == null)
            return;
        switch (activeStation.GetState())
        {
            case EStationState.Available:
            case EStationState.Repairing:
                gameObject.SetActive(false);
                break;
            case EStationState.Booked:
                gameObject.SetActive(true);
                tmp.text = "开始使用";
                break;
            case EStationState.Ocuppied:
                gameObject.SetActive(true);
                tmp.text = "使用完毕";
                break;
        }
    }

    protected override void OnClick()
    {
        switch (activeStation.GetState())
        {
            case EStationState.Booked:
                SetState(EStationState.Ocuppied);
                break;
            case EStationState.Ocuppied:
                SetState(EStationState.Available);
                break;
            default:
                break;
        }
    }
}
