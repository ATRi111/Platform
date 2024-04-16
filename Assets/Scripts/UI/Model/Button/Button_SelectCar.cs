using TMPro;

public class Button_SelectCar : MyButton
{
    private UserDataManager dataManager;
    private string carType;

    protected override void Awake()
    {
        base.Awake();
        dataManager = DataManager.FindInstance<UserDataManager>();
        carType = GetComponentInChildren<TextMeshProUGUI>().text;
    }

    protected override void OnClick()
    {
        dataManager.ModifyCarType(carType);
    }
}
