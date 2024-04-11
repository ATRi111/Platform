using Unity.Netcode;
using UnityEngine;

public class Button_Fault : MyButton
{
    [SerializeField]
    private FaultRecordPanel recordPanel;

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
