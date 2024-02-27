using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Button_StartHost : MyButton
{
    protected override void OnClick()
    {
        NetworkManager.Singleton.StartHost();
    }
}
