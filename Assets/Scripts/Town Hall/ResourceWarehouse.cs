using System.Collections.Generic;
using UnityEngine;

public class ResourceWarehouse : MonoBehaviour
{
    private List<Resource> _resourcesOfTownHall = new List<Resource>();

    public int Count => _resourcesOfTownHall.Count;

    public void AddResource(Resource resource)
    {
        _resourcesOfTownHall.Add(resource);
    }

    public void RemoveAt(int index)
    {
        _resourcesOfTownHall.RemoveAt(index);
    }
}
