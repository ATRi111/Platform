using Services;
using Services.Event;
using System.Text;
using TMPro;
using Tools;
using UnityEngine;

public class MessageWindow : MonoBehaviour
{
    private IEventSystem eventSystem;
    private TextMeshProUGUI tmp;
    private MyCanvasGrounp canvasGrounp;
    private ChargingStation current;

    private void Awake()
    {
        eventSystem = ServiceLocator.Get<IEventSystem>();
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        canvasGrounp = GetComponent<MyCanvasGrounp>();
    }

    private void Update()
    {
        if(canvasGrounp.Visible && Input.GetMouseButtonDown(0))
        {
            eventSystem.Invoke(EEvent.SelectStation, current);
            Hide();
        }
    }

    private void OnEnable()
    {
        eventSystem.AddListener<ChargingStation, Vector3>(EEvent.ShowChargingStaionMessageWindow, Show);
        eventSystem.AddListener(EEvent.HideChargingStaionMessageWindow, Hide);
    }

    private void OnDisable()  
    {
        eventSystem.RemoveListener<ChargingStation, Vector3>(EEvent.ShowChargingStaionMessageWindow, Show);
        eventSystem.RemoveListener(EEvent.HideChargingStaionMessageWindow, Hide);
    }

    private void Show(ChargingStation station, Vector3 position)
    {
        current = station;
        transform.position = Camera.main.WorldToScreenPoint(position);
        StringBuilder sb = new StringBuilder();
        string s = UsageData.StateName(station.GetState());
        s.FontSize(32);
        sb.AppendLine(s);
        sb.AppendLine("查看详情");
        tmp.text = sb.ToString();
        canvasGrounp.Visible = true;
    }

    private void Hide()
    {
        current = null;
        canvasGrounp.Visible = false;
    }
}