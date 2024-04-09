using Services;
using Services.Event;
using UnityEngine;

public abstract class DataPanel : MonoBehaviour
{
    protected MyCanvasGrounp canvasGrounp;
    protected IEventSystem eventSystem;
    protected ChargingStation activeStation;

    protected virtual void Awake()
    {
        eventSystem = ServiceLocator.Get<IEventSystem>();
        canvasGrounp = GetComponent<MyCanvasGrounp>();
        eventSystem.AddListener<ChargingStation>(EEvent.SelectStation, AfterSelectStation);
        eventSystem.AddListener(EEvent.Refresh, Refresh);
    }

    protected virtual void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Hide();
        }
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
        activeStation = null;
        canvasGrounp.Visible = false;
    }
}
