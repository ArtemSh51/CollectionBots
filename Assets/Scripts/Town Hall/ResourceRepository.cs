using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceRepository : MonoBehaviour
{
    private List<Resource> _foldableResources = new List<Resource>();

    public int Count => _foldableResources.Count;

    public void AddResource(Resource resource)
    {
        _foldableResources.Add(resource);
    }

    public void RemoveResource(int index)
    {
        _foldableResources.RemoveAt(index);
    }

    public void Clear()
    {
        _foldableResources.Clear();
    }
}
