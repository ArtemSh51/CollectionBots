using System.Collections.Generic;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private int _countOfResources;

    private Collider[] _colliders;
    private List<Resource> _scannedResources = new List<Resource>();

    public IReadOnlyList<Resource> ScannedResources => _scannedResources;

    private void Awake()
    {
        _colliders = new Collider[_countOfResources];
    }

    private void Update()
    {
        _scannedResources.Clear();

        int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders);

        for (int i = 0; i < count; i++)
        {
            if (_colliders[i].TryGetComponent(out Resource resource))
            {
                _scannedResources.Add(resource);
            }
        }
    }

    public Transform GetResourcePosition(int index)
    {
        return _scannedResources[index].transform;
    }
}
