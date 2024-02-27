using MyTimer;
using UnityEngine;

//������Ҫ����-��ʾ��UI������
[RequireComponent(typeof(CanvasGroup))]
public class MyCanvasGrounp : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    private LinearTransformation linear;
    protected float alpha_default;

    [SerializeField]
    protected float fadeTime = 0.2f;

    /// <summary>
    /// ��һ����ʾ/�����Ƿ��������
    /// </summary>
    public bool immediate_next;
    /// <summary>
    /// ��ʾ/�����Ƿ�һֱ���������
    /// </summary>
    public bool immediate;
    [SerializeField]
    private bool visible_init;

    private bool visible;
    public bool Visible
    {
        get => visible;
        set
        {
            if (value != visible || immediate_next)
            {
                visible = value;
                SetVisibleAndActive();
            }
        }
    }

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        linear = new LinearTransformation();
        linear.OnTick += SetAlpha;
        linear.AfterCompelete += SetAlpha;
        alpha_default = canvasGroup.alpha;
        immediate_next = true;
        visible = !visible_init;
        Visible = visible_init;
    }

    protected void SetVisibleAndActive()
    {
        canvasGroup.interactable = visible;
        canvasGroup.blocksRaycasts = visible;
        float target = visible ? alpha_default : 0f;
        linear.Initialize(canvasGroup.alpha, target, fadeTime);
        if (immediate || immediate_next)
        {
            linear.ForceComplete();
            immediate_next = false;
        }
    }

    private void SetAlpha(float alpha)
    {
        canvasGroup.alpha = alpha;
    }
}
