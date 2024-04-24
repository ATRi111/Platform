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
        error.text = string.Empty;
        phoneNumber.text = phoneNumber.text.Trim();
        password.text = password.text.Trim();
        if (!long.TryParse(phoneNumber.text, out long _))
        {
            error.text = "不合法的手机号";
            return;
        }
        if (password.text.Length < 8 || password.text.Length > 16)
        {
            error.text = "密码长度必须为8-16位";
            return;
        }
        if (signUp)
        {
            if (dataManager.userDict.ContainsKey(phoneNumber.text))
            {
                error.text = "手机号已存在";
            }
            else
            {
                dataManager.NewUser(phoneNumber.text, password.text);
                dataManager.localPhoneNumber = phoneNumber.text;
                Destroy(panel);
            }
        }
        else
        {
            if(!dataManager.userDict.ContainsKey(phoneNumber.text))
            {
                error.text = "手机号不存在";
            }
            else if (dataManager.userDict[phoneNumber.text].Password != password.text)
            {
                error.text = "密码错误";
            }
            else
            {
                dataManager.localPhoneNumber = phoneNumber.text;
                Destroy(panel);
            }
        }
    }
}
