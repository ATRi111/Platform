using Unity.Netcode;

public class Button_Shutdown : MyButton
{
    protected override void OnClick()
    {
        NetworkManager.Singleton.Shutdown();
    }
}
