using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ResourceSpawner : MonoBehaviour
{
    [SerializeField] private Resource _resourcePrefab;

    [SerializeField] float _minXPoition;
    [SerializeField] float _maxXPoition;

    [SerializeField] float _minZPosition;
    [SerializeField] float _maxZPosition;

    [SerializeField] float _yPosition;

    [SerializeField] private int _defaultCountOfResources;
    [SerializeField] private int _maxSize;
    [SerializeField] private float _timeBetweenResourceCreation;

    private ObjectPool<Resource> _resourcePool;
    private bool _canSpawnResources = true;

    private void Awake()
    {
        _resourcePool = new ObjectPool<Resource>
        (
            createFunc: () => Instantiate(_resourcePrefab),
            actionOnGet: (resource) => ConfigureResourceBeforeUse(resource),
            actionOnRelease: (resource) => ConfigureResourceAfterUse(resource),
            actionOnDestroy: (resource) => Destroy(resource.gameObject),
            collectionCheck: true,
            defaultCapacity: _defaultCountOfResources,
            maxSize: _maxSize
        );
    }

    private void Start()
    {
        StartCoroutine(CreateResource());
    }

    public void ReturnResource(Resource resource)
    {
        _resourcePool.Release(resource);
    }

    private void ConfigureResourceBeforeUse(Resource resource)
    {
        resource.SetColliderEnabled(true);
        resource.SetNavMeshObstacleEnambled(true);
        resource.gameObject.SetActive(true);
    }

    private void ConfigureResourceAfterUse(Resource resource)
    {
        resource.gameObject.SetActive(false);

        resource.transform.position = Vector3.zero;
        resource.transform.rotation = Quaternion.identity;
    }

    private IEnumerator CreateResource()
    {
        WaitForSeconds wait = new WaitForSeconds(_timeBetweenResourceCreation);

        while (_canSpawnResources)
        {
            Resource resource = _resourcePool.Get();

            float randomX = Random.Range(_minXPoition, _maxXPoition);
            float randomZ = Random.Range(_minZPosition, _maxZPosition);

            resource.transform.position = new Vector3(randomX, _yPosition, randomZ);

            yield return wait;
        }
    }
}
