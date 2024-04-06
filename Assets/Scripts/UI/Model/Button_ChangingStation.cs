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
        eventSystem.AddListener<ChargingStation>(EEvent.OpenChargingStationPanel, Refresh);
    }

    protected virtual void OnDestroy()
    {
        eventSystem.RemoveListener<ChargingStation>(EEvent.OpenChargingStationPanel, Refresh);
    }

    protected virtual void Refresh(ChargingStation station)
    {
        activeStation = station;
    }


    protected void SetState(EStationState state)
    {
        stationDataManager.InsertUsageRpc(userDataManager.LocalUserPhoneNumber(), activeStation.data.Id, state);
    }
}
