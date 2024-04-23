using TMPro;
using UnityEngine;

public class Button_SignIn : MyButton
{
    [SerializeField]
    private TMP_InputField phoneNumber;
    [SerializeField]
    private TMP_InputField password;
    [SerializeField]
    private GameObject panel;
    [SerializeField]
    private TextMeshProUGUI error;

    private UserDataManager dataManager;

    public bool signUp;

    protected override void Awake()
    {
        base.Awake();
        dataManager = DataManager.FindInstance<UserDataManager>();
    }

    protected override void OnClick()
    {
        if (!int.TryParse(phoneNumber.text, out int number) || number.ToString().Length != 11)
        {
            error.text = "不合法的手机号";
        }
        if (signUp)
        {
            if (dataManager.userDict.ContainsKey(number))
            {
                error.text = "手机号已存在";
            }
            else
            {
                dataManager.NewUser(number, password.text);
                dataManager.localPhoneNumber = number;
                Destroy(panel);
            }
        }
        else
        {
            if(!dataManager.userDict.ContainsKey(number))
            {
                error.text = "手机号不存在";
            }
            else if (dataManager.userDict[number].Password != password.text)
            {
                error.text = "密码错误";
            }
            else
            {
                dataManager.localPhoneNumber = number;
                Destroy(panel);
            }
        }
    }
}
