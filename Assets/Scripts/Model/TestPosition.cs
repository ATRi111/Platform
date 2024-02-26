using UnityEngine;

public class TestPosition : MonoBehaviour
{
    public int stateIndex;
    public int StateIndex
    {
        get => stateIndex;
        set
        {
            stateIndex = value;
            material.color = value switch
            {
                1 => Color.green,
                2 => Color.blue,
                3 => Color.red,
                _ => Color.white,
            };
        }
    }

    private Material material;

    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
    }
}
