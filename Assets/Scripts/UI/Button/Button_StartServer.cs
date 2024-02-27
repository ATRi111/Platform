using Unity.Netcode;

public class Button_StartServer : MyButton
{
    protected override void OnClick()
    {
        NetworkManager.Singleton.StartServer();
    }
}
