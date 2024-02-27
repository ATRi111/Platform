using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public abstract class MySlider : MonoBehaviour
{
    protected Slider m_slider;
    [SerializeField]
    protected float init;

    protected virtual void Awake()
    {
        m_slider = GetComponent<Slider>();
        m_slider.onValueChanged.AddListener(OnSlide);
        m_slider.value = init;
    }

    protected abstract void OnSlide(float value);
}
