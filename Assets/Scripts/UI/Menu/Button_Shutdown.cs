using Unity.Netcode;
using UnityEngine.SceneManagement;

public class Button_Shutdown : MyButton
{
    protected override void OnClick()
    {
        NetworkManager.Singleton.Shutdown();
        NetworkManager.Singleton.SceneManager.LoadScene("0", LoadSceneMode.Single);
    }
}
