using Services;
using Services.Event;
using TMPro;
using UnityEngine;

public class ChargingStationPanel : MonoBehaviour
{
    private IEventSystem eventSystem;
    [SerializeField]
    private TextMeshProUGUI tmp;
    private MyCanvasGrounp canvasGrounp;

    private void Awake()
    {
        eventSystem = ServiceLocator.Get<IEventSystem>();
        canvasGrounp = GetComponent<MyCanvasGrounp>();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape))
        {
            Hide();
        }
    }

    private void OnEnable()
    {
        eventSystem.AddListener<ChargingStation>(EEvent.OpenChargingStationPanel, Show);
    }

    private void OnDisable()
    {
        eventSystem.RemoveListener<ChargingStation>(EEvent.OpenChargingStationPanel, Show);
    }

    private void Show(ChargingStation station)
    {
        if (station == null)
            return;
        ChargingStationData data = station.data;
        tmp.text = $"编号:{data.Id}\n" +
            $"充电桩类型:{data.Type}\n" +
            $"输入电压:{data.Voltage}\n" +
            $"输出功率:{data.Power:F1}kW\n" +
            $"电价:{data.Price:F2}元/度\n";
        canvasGrounp.Visible = true;
    }

    private void Hide()
    {
        canvasGrounp.Visible = false;
    }
}