using TMPro;
using UnityEngine;

public class UserPanel : DataPanel
{
    [SerializeField]
    private TextMeshProUGUI message;
    [SerializeField]
    private TextMeshProUGUI hint;
    private UserDataManager userDataManager;
    private StationDataManager stationDataManager;
    private UserData localData;

    protected override void Awake()
    {
        base.Awake();
        userDataManager = DataManager.FindInstance<UserDataManager>();
        stationDataManager = DataManager.FindInstance<StationDataManager>();
    }

    public override void Refresh()
    {
        localData = userDataManager.userDict[userDataManager.localPhoneNumber];
        if(localData != null )
        {
            message.text = $"手机号:{localData.PhoneNumber}\n" +
                $"车型:{localData.Model}\n";
            if (stationDataManager.OccupyAny())
                hint.text = "正在使用充电桩，无法修改车型";
            else
                hint.text = "点击下方按钮选择车型";
        }
    }
}
