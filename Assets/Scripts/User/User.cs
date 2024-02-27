using Unity.Netcode;
using UnityEngine;

public class User : NetworkBehaviour
{
    //private ChargingStation station;

    //private void Start()
    //{
    //    station = GameObject.Find(nameof(ChargingStation)).GetComponent<ChargingStation>();
    //    transform.position = Camera.main.transform.position;
    //}

    //private void Update()
    //{
    //    if (IsClient && IsOwner)
    //    {
    //        if (Input.GetKeyDown(KeyCode.S))
    //        {
    //            TestServerRpc();
    //        }
    //    }
    //}

    //[ServerRpc]
    //private void TestServerRpc()
    //{
    //    station.Use();
    //}
}
