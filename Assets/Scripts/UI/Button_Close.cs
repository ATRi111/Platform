using UnityEngine;

public class Button_Close : MyButton
{
    private MyCanvasGrounp canvasGrounp;

    protected override void Awake()
    {
        base.Awake();
        canvasGrounp = GetComponentInParent<MyCanvasGrounp>();
    }

    protected override void OnClick()
    {
        Debug.Log(1);
        canvasGrounp.immediate_next = true;
        canvasGrounp.Visible = false;
    }
}
