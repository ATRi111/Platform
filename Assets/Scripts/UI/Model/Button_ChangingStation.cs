using Services.Event;
using TMPro;

public abstract class Button_ChangingStation : MyButton
{
    protected ChargingStation activeStation;
    protected TextMeshProUGUI tmp;
    protected UserDataManager userDataManager;
    protected StationDataManager stationDataManager;

    protected override void Awake()
    {
        base.Awake();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        userDataManager = DataManager.FindInstance<UserDataManager>();
        stationDataManager = DataManager.FindInstance<StationDataManager>();
        eventSystem.AddListener<ChargingStation>(EEvent.OpenChargingStationPanel, Show);
        eventSystem.AddListener(EEvent.Refresh, Refresh);
    }

    protected virtual void OnDestroy()
    {
        eventSystem.RemoveListener<ChargingStation>(EEvent.OpenChargingStationPanel, Show);
        eventSystem.RemoveListener(EEvent.Refresh, Refresh);
    }

    protected void Show(ChargingStation station)
    {
        activeStation = station;
        Refresh();
    }

    protected abstract void Refresh();

    protected void SetState(EStationState state)
    {
        stationDataManager.InsertUsage(userDataManager.LocalUserPhoneNumber(), activeStation.data.Id, state);
    }
}
