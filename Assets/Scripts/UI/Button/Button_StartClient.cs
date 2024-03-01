using Services.SceneManagement;
using Services;
using Unity.Netcode;

public class Button_StartClient : MyButton
{
    protected override void OnClick()
    {
        NetworkManager.Singleton.StartClient();
        ServiceLocator.Get<ISceneController>().LoadScene(2);
    }
}
