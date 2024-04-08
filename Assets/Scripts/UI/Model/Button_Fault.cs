using Unity.Netcode;

public class Button_Fault : MyButton
{
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

    }
}
