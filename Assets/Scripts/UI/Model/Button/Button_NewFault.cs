public class Button_NewFault : Button_ChangingStation
{
    private ModifyFaultPanel panel;

    protected override void Awake()
    {
        base.Awake();
        panel = GetComponentInParent<ModifyFaultPanel>();
    }

    protected override void Refresh()
    {
        if (activeStation == null)
            return;
        switch(activeStation.GetState())
        {
            case EStationState.Available:
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
