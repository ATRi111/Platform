using Services;
using Services.SceneManagement;
using Unity.Netcode;

public class Button_StartHost : MyButton
{
    protected override void OnClick()
    {
        NetworkManager.Singleton.StartHost();
        ServiceLocator.Get<ISceneController>().LoadScene(2);
    }
}
