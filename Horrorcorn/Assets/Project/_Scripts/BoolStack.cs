using System.Collections.Generic;
using UnityEngine;

public class BoolStack
{
    private HashSet<GameObject> stack = new HashSet<GameObject>();

    public void Add(GameObject obj)
    {
        stack.Add(obj);
    }

    public void Remove(GameObject obj)
    {
        stack.Remove(obj);
    }

    public bool IsSet
    {
        get { return stack.Count > 0; }
    }
}
