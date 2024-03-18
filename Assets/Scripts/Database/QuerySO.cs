using UnityEngine;

[CreateAssetMenu(fileName = "NewQuery",menuName = "Query")]
public class QuerySO : ScriptableObject
{
    [TextArea(3,10)]
    public string content;
}
