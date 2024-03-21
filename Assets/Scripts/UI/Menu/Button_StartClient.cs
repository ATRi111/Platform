using Unity.Netcode;
using UnityEngine.SceneManagement;

public class Button_StartClient : MyButton
{
    protected override void OnClick()
    {
        NetworkManager.Singleton.StartClient();
        NetworkManager.Singleton.SceneManager.LoadScene("Model", LoadSceneMode.Single);
    }
}
