using Services;
using Services.Event;
using Tools;
using Unity.Netcode;
using UnityEngine;

public abstract class DataPanel : MonoBehaviour
{
    protected static string EmphasizedText(object obj)
        => obj.ToString().ColorText("blue");

    protected MyCanvasGrounp canvasGrounp;
    protected IEventSystem eventSystem;
    protected ChargingStation activeStation;
    protected bool IsServer => NetworkManager.Singleton.IsServer;
    public bool Visible => canvasGrounp.Visible;

    protected virtual void Awake()
    {
        eventSystem = ServiceLocator.Get<IEventSystem>();
        canvasGrounp = GetComponent<MyCanvasGrounp>();
        eventSystem.AddListener<ChargingStation>(EEvent.SelectStation, AfterSelectStation);
        eventSystem.AddListener(EEvent.Refresh, Refresh);
    }

    protected virtual void Update()
    {

    }

    protected virtual void OnDestroy()
    {
        eventSystem.RemoveListener(EEvent.Refresh, Refresh);
        eventSystem.RemoveListener<ChargingStation>(EEvent.SelectStation, AfterSelectStation);
    }

    protected virtual void AfterSelectStation(ChargingStation station)
    {
        activeStation = station;
    }

    public virtual void Show()
    {
        Refresh();
        canvasGrounp.Visible = true;
    }

    public abstract void Refresh();

    public virtual void Hide()
    {
        canvasGrounp.Visible = false;
    }
}
