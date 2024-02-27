using Unity.Netcode;
using UnityEngine;

public class ChargingStation : NetworkBehaviour
{
    public NetworkVariable<float> electricity;

    public void Update()
    {
        if (IsServer)
        {
            electricity.Value = Mathf.Clamp(electricity.Value + 0.01f * Time.deltaTime, 0f, 1f);
        }
    }

    public void Use()
    {
        if(IsServer)
        {
            electricity.Value = Mathf.Clamp(electricity.Value - 0.1f, 0f, 1f);
        }
    }
}