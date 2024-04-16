public class Button_Use : Button_ChangingStation
{
    private DataPanelController controller;

    protected override void Awake()
    {
        base.Awake();
        controller = GetComponentInParent<DataPanelController>();
    }

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
                gameObject.SetActive(activeStation.IsLocalUser());
                tmp.text = "开始使用";
                break;
            case EStationState.Ocuppied:
                gameObject.SetActive(activeStation.IsLocalUser());
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
                controller.Hide();
                break;
            case EStationState.Ocuppied:
                SetState(EStationState.Available);
                break;
            default:
                break;
        }
    }
}
