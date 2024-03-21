using Unity.Netcode;
using UnityEngine.SceneManagement;

public class Button_StartHost : MyButton
{
    protected override void OnClick()
    {
        NetworkManager.Singleton.StartHost();
        NetworkManager.Singleton.SceneManager.LoadScene("Model", LoadSceneMode.Single);
    }
}
