using UnityEngine;

[CreateAssetMenu(fileName = "NewQuery",menuName = "Query")]
public class QuerySO : ScriptableObject
{
    [TextArea(3,10)]
    public string content;

    public string ReplaceArguments(params string[] args)
    {
        string copy = content;
        for (int i = 0; i < args.Length; i++)
        {
            copy.Replace($"arg{i}", args[i]);
        }
        return copy;
    }
}
