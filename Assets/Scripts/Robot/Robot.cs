using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Robot : MonoBehaviour
{
    [SerializeField] private Transform _townHall;
    [SerializeField] private ResourceRepository _resourceRepository;
    [SerializeField] private ResourceSpawner _spawner;
    [SerializeField] private RobotToucher _toucher;
    [SerializeField] private Builder _builder;
    [SerializeField] private float _distanceToTownHall;
    [SerializeField] private float _distanceToFlag;

    private Vector3 _currentResource;

    private NavMeshAgent _agent;
    private Vector3 _flag;

    private bool _isBusy;
    private bool _isFlagUp;

    public bool IsBusy => _isBusy;
    public bool IsFlagUp => _isFlagUp;

    public ResourceSpawner ResourceSpawner => _spawner;

    public Vector3 Flag => _flag;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (_isBusy == false)
        {
            _agent.SetDestination(_currentResource);
        }
        else if (_isBusy == true)
        {
            GoToTownHall();
        }

        if (_isFlagUp && _isBusy == false && _resourceRepository.Count >= _builder.CostOfTownHall)
        {
            _agent.SetDestination(_flag);
        }
    }

    public void SetBusy(bool isBusy)
    {
        _isBusy = isBusy;
    }

    public void SetFlagUp(bool isFalgUp, Vector3 flagPosition)
    {
        _isFlagUp = isFalgUp;
        _flag = flagPosition;
    }

    public void SetTownHallTransform(Transform townHall)
    {
        _townHall = townHall;
    }

    public void SetResourceRepository(ResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
    }

    public void SetResourceSpawner(ResourceSpawner resourceSpawner)
    {
        _spawner = resourceSpawner;
    }

    public void Move(Vector3 resource)
    {
        _currentResource = resource;
    }

    private void GoToTownHall()
    {
        _agent.SetDestination(_townHall.position);

        if (Vector3.Distance(transform.position, _townHall.position) <= _distanceToTownHall)
        {
            _isBusy = false;

            _spawner.ReturnResource(_toucher.Resource);

            _resourceRepository.AddResource(_toucher.Resource);

            _toucher.Resource.transform.parent = null;
        }
    }
}
