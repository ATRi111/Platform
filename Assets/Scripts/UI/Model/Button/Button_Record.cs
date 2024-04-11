using Unity.Netcode;
using UnityEngine;

public class Button_Record : MyButton
{
    [SerializeField]
    private UseRecordPanel recordPanel;

    protected override void Awake()
    {
        base.Awake();
        if (!NetworkManager.Singleton.IsServer)
        {
            Destroy(gameObject);
        }
    }

    protected override void OnClick()
    {
        recordPanel.Show();
    }
}
