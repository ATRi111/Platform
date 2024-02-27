using TMPro;
using Unity.Netcode;

public class ChargingStationUI : NetworkBehaviour
{
    private TextMeshProUGUI tmp;
    private ChargingStation station;

    private void Awake()
    {
        station = GetComponentInParent<ChargingStation>();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        tmp.text = $"{(int)(station.electricity.Value * 100)}%";
    }
}
