using Unity.Netcode;
using UnityEngine;

public class Button_Shutdown : MyButton
{
    protected override void OnClick()
    {
        NetworkManager.Singleton.Shutdown();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
