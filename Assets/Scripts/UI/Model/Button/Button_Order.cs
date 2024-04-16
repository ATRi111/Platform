public class Button_Order : Button_ChangingStation
{
    protected override void Refresh()
    {
        if (activeStation == null)
            return;
        switch (activeStation.GetState())
        {
            case EStationState.Available:
                gameObject.SetActive(true);
                tmp.text = "预订";
                break;
            case EStationState.Repairing:
                gameObject.SetActive(false);
                break;
            case EStationState.Booked:
                gameObject.SetActive(activeStation.IsLocalUser());
                tmp.text = "取消预订";
                break;
            case EStationState.Ocuppied:
                gameObject.SetActive(false);
                break;
        }
    }

    protected override void OnClick()
    {
        switch (activeStation.GetState())
        {
            case EStationState.Booked:
                SetState(EStationState.Available);
                break;
            case EStationState.Available:
                SetState(EStationState.Booked);
                break;
            default:
                break;
        }
    }
}
