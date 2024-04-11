using UnityEngine;

public class DataPanelController : MonoBehaviour
{
    private DataPanel[] dataPanels;

    private void Awake()
    {
        dataPanels = GetComponentsInChildren<DataPanel>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            for (int i = dataPanels.Length - 1; i >= 0; i--)
            {
                if (dataPanels[i].Visible)
                {
                    dataPanels[i].Hide();
                    break;
                }
            }
        }
    }
}
