using UnityEngine;

public class Button_UserPanel : MyButton
{
    [SerializeField]
    private UserPanel userPanel;

    protected override void OnClick()
    {
        userPanel.Show();
    }
}
