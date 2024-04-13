using TMPro;

public class UserPanel : DataPanel
{
    private TextMeshProUGUI tmp;
    private UserDataManager dataManager;
    private UserData localData;

    protected override void Awake()
    {
        base.Awake();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        dataManager = DataManager.FindInstance<UserDataManager>();
    }

    public override void Refresh()
    {
        localData = dataManager.userDict[dataManager.LocalUserPhoneNumber()];
        if(localData != null )
        {
            tmp.text = $"手机号:{localData.PhoneNumber}\n" +
                $"车型:{localData.Model}\n";
        }
    }
}
