using UnityEngine;

public class Creator : MonoBehaviour
{
    [SerializeField] private Robot _robotPrefab;
    [SerializeField] private Transform _spawner;
    [SerializeField] private Transform _townHall;
    [SerializeField] private ResourceRepository _resourceRepository;
    [SerializeField] private RobotStorage _storage;
    [SerializeField] private ResourceSpawner _resourceSpawner;
    [SerializeField] private int _costOfRobot;

    private void Update()
    {
        if (_resourceRepository.Count == _costOfRobot)
        {
            CreateRobot();
        }
    }

    public void CreateRobot()
    {
        Robot newRobot = Instantiate(_robotPrefab, _spawner.position, Quaternion.identity);

        _storage.AddRobot(newRobot);

        newRobot.SetResourceRepository(_resourceRepository);
        newRobot.SetResourceSpawner(_resourceSpawner);
        newRobot.SetTownHallTransform(_townHall);

        Buy(_costOfRobot);
    }

    private void Buy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            _resourceRepository.RemoveResource(i);
        }
    }
}
