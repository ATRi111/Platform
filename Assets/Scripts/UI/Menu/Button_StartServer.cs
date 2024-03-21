using Unity.Netcode;
using UnityEngine.SceneManagement;
public class Button_StartServer : MyButton
{
    protected override void OnClick()
    {
        NetworkManager.Singleton.StartServer();
        NetworkManager.Singleton.SceneManager.LoadScene("Model", LoadSceneMode.Single);
    }
}
