using Unity.Netcode;
using UnityEngine;

public class TestPosition : NetworkBehaviour
{
    public NetworkVariable<int> stateIndex;

    private Material material;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        stateIndex.OnValueChanged += AfterIndexChange;
        AfterIndexChange(default, stateIndex.Value);
    }

    private void AfterIndexChange(int prev,int value)
    {
        material.color = value switch
        {
            1 => Color.green,
            2 => Color.blue,
            3 => Color.red,
            _ => Color.white,
        };
    }
}