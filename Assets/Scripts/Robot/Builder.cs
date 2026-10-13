using UnityEngine;

public class Builder : MonoBehaviour
{
    [SerializeField] private Transform _townHallPrefab;
    [SerializeField] private ResourceRepository _resourceRepository;
    [SerializeField] private RobotStorage _robotStorage;
    [SerializeField] private Robot _robot;
    [SerializeField] private int _costOfTownHall = 5;
    [SerializeField] private int _countOfRobots = 1;
    [SerializeField] private int _distanceFromRobotToFlag = 1;

    public int CostOfTownHall => _costOfTownHall;

    private void Awake()
    {
        _robot = GetComponent<Robot>();
    }

    private void Update()
    {
        if (_resourceRepository.Count < _costOfTownHall) 
            return;

        if (!_robot.IsFlagUp) 
            return;

        if (_robotStorage.Robots.Count <= _countOfRobots) 
            return;

        float distance = Vector3.Distance(_robot.transform.position, _robot.Flag);
        if (distance > _distanceFromRobotToFlag) 
            return;

        CreateTownHall();
    }

    public void SetResourceRepository(ResourceRepository resourceRepository)
    {
        _resourceRepository = resourceRepository;
    }

    public void SetRobotStorage(RobotStorage robotStorage)
    {
        _robotStorage = robotStorage;
    }

    private void CreateTownHall()
    {
        _robot.SetFlagUp(false, _robot.transform.position);

        Transform newTownHall = Instantiate(_townHallPrefab, new Vector3(_robot.Flag.x, 1.3f, _robot.Flag.z), Quaternion.identity);

        if (newTownHall.TryGetComponent(out RobotStorage robotStorage))
        {
            robotStorage.Clear();
            robotStorage.AddRobot(_robot);
        }

        if (newTownHall.TryGetComponent(out ResourceRepository resourceRepository))
        {
            resourceRepository.Clear();
        }

        _robotStorage.RemoveRobot(0);

        if (newTownHall.TryGetComponent(out Creator creator))
        {
            creator.SetResourceSpawner(_robot.ResourceSpawner);
        }

        Buy(_costOfTownHall);

        ReassignRobot(newTownHall);
    }

    private void Buy(int amount)
    {
        int count = Mathf.Min(amount, _resourceRepository.Count);

        for (int i = 0; i < count; i++)
        {
            _resourceRepository.RemoveResource(0);
        }
    }

    private void ReassignRobot(Transform newTownHallTransform)
    {
        _robot.SetTownHallTransform(newTownHallTransform);

        if (newTownHallTransform.TryGetComponent(out ResourceRepository resourceRepository))
        {
            _robot.SetResourceRepository(resourceRepository);
            _resourceRepository = resourceRepository;
        }
        
        if (newTownHallTransform.TryGetComponent(out RobotStorage robotStorage))
        {
            _robotStorage = robotStorage;
        }
    }
}
