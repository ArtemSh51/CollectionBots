using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceScanner : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private int _countOfColliders;

    [SerializeField] private float _timeBetweenListUpdates;

    private Collider[] _colliders;
    private List<Resource> _resources = new List<Resource>();

    private bool _canListUpdated = true;

    public IReadOnlyList<Resource> Resources => _resources;

    private void Awake()
    {
        _colliders = new Collider[_countOfColliders];

        StartCoroutine(ScanResources());
    }

    private IEnumerator ScanResources()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeBetweenListUpdates);

        while (_canListUpdated)
        {
            _resources.Clear();

            int count = Physics.OverlapSphereNonAlloc(transform.position, _radius, _colliders);

            for (int i = 0; i < count; i++)
            {
                if (_colliders[i].TryGetComponent(out Resource resource))
                {
                    _resources.Add(resource);
                }
            }

            yield return wait;
        }
    }

    public Vector3 GetPosition(int index)
    {
        return _resources[index].transform.position;
    }
}
