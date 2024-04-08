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
        eventSystem.AddListener(EEvent.Refresh, Refresh);
    }

    protected virtual void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape) || Input.GetMouseButtonDown(1))
        {
            Hide();
        }
    }

    protected virtual void OnDestroy()
    {
        eventSystem.RemoveListener(EEvent.Refresh, Refresh);
    }

    public virtual void Show(ChargingStation station)
    {
        activeStation = station;
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
