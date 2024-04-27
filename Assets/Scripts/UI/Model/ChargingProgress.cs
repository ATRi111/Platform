using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChargingProgress : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI rateTmp;
    [SerializeField]
    private Image rateImage;

    public void SetRate(float rate)
    {
        rateImage.fillAmount = rate;
        rateTmp.text = rate.ToString("p0");
    }
}
