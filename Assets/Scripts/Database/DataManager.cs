using Services;
using Services.Event;
using Unity.Netcode;

public abstract class DataManager : NetworkBehaviour
{
    protected new static bool IsServer => NetworkManager.Singleton.IsServer;
    protected IEventSystem eventSystem;
    protected DatabaseManager databaseManager;

    protected virtual void Awake()
    {
        eventSystem = ServiceLocator.Get<IEventSystem>();
        if (IsServer)
            databaseManager = DatabaseManager.FindInstance();
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        eventSystem.AddListener(EEvent.Refresh, UpdateData);
    }

    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        eventSystem.RemoveListener(EEvent.Refresh, UpdateData);
    }

    protected virtual void Start()
    {

    }

    protected abstract void UpdateData();
}
