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

    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Transform _flagPrefab;

    private Ray _ray;
    private RaycastHit _hit;
    private bool _isTownHallSelect;

    private void OnEnable()
    {
        _inputReader.HighlightTownHallButtonPressed += HighlightTownHall;
        _inputReader.FlagCreationButtonPressed += CreateFlag;
    }

    private void Update()
    {
        _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (_resourceRepository.Count >= _costOfRobot && _storage.Robots[0].IsFlagUp == false)
        {
            Buy(_costOfRobot);
            CreateRobot();
        }  
    }

    private void OnDisable()
    {
        _inputReader.HighlightTownHallButtonPressed -= HighlightTownHall;
        _inputReader.FlagCreationButtonPressed -= CreateFlag;
    }

    public void SetResourceSpawner(ResourceSpawner resourceSpawner)
    {
        _resourceSpawner = resourceSpawner;
    }

    private void HighlightTownHall()
    {
        if (_isTownHallSelect == false && Physics.Raycast(_ray, out _hit) && _hit.collider.gameObject.TryGetComponent(out Creator creator))
        {
            _isTownHallSelect = true;
        }
    }

    private void CreateRobot()
    {
        for (int i = 0; i < _storage.Robots.Count; i++)
        {
            if (_storage.Robots[i].IsFlagUp)
            {
                return;
            }
        }

        Robot newRobot = Instantiate(_robotPrefab, _spawner.position, Quaternion.identity);

        _storage.AddRobot(newRobot);

        newRobot.SetResourceRepository(_resourceRepository);
        newRobot.SetResourceSpawner(_resourceSpawner);
        newRobot.SetTownHallTransform(_townHall);

        Builder builder = newRobot.GetComponent<Builder>();

        builder.SetResourceRepository(_resourceRepository);
        builder.SetRobotStorage(_storage);
    }

    private void CreateFlag()
    {
        if (_isTownHallSelect && Physics.Raycast(_ray, out _hit))
        {
            Transform newFlag = Instantiate(_flagPrefab, _hit.point, Quaternion.identity);

            _isTownHallSelect = false;

            _storage.Robots[0].SetFlagUp(true, newFlag.position);
        }
    }

    private void Buy(int amount)
    {
        int count = Mathf.Min(amount, _resourceRepository.Count);

        for (int i = 0; i < count; i++)
        {
            _resourceRepository.RemoveResource(0);
        }
    }
}
