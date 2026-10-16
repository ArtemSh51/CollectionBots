using UnityEngine;

public class Creator : MonoBehaviour
{
    [SerializeField] private Transform _spawner;
    [SerializeField] private Robot _robotPrefab;
    [SerializeField] private Flag _flag;
    [SerializeField] private int _robotPrice;
    [SerializeField] private ResourceSpawner _resourceSpawner;

    [SerializeField] private Transform _townHallPrefab;
    [SerializeField] private int _townHallPrice;

    private void Update()
    {
        CreateRobot();
        CreateTownHall();
    }

    public void SetFlag(Flag flag)
    {
        _flag = flag;
    }

    public void SetResourceSpawner(ResourceSpawner resourceSpawner)
    {
        _resourceSpawner = resourceSpawner;
    }

    private void CreateRobot()
    {
        if (_flag.gameObject.activeSelf == false && transform.TryGetComponent(out ResourceWarehouse resourceWarehouse))
        {
            if (resourceWarehouse.Count >= _robotPrice)
            {
                Buy(_robotPrice);

                Robot newRobot = Instantiate(_robotPrefab, _spawner.position, Quaternion.identity);
                newRobot.SetFlag(_flag);
                newRobot.SetResourceSpawner(_resourceSpawner);
                newRobot.SetTownHallTransform(transform);

                if (newRobot.TownHallMover != null)
                    newRobot.TownHallMover.ReassignTownHall(transform);

                transform.GetComponent<RobotWarehouse>().AddRobot(newRobot);
            }
        }
    }

    private void CreateTownHall()
    {
        if (_flag.HasFlagTaken == true && transform.TryGetComponent(out ResourceWarehouse resourceWarehouse) 
            && transform.TryGetComponent(out RobotWarehouse robots))
        {
            if (resourceWarehouse.Count >= _townHallPrice && robots.Count >= 2)
            {
                Buy(_townHallPrice);

                Vector3 position = new Vector3(_flag.transform.position.x, 0.98f, _flag.transform.position.z);

                _townHallPrefab = Instantiate(_townHallPrefab, position, Quaternion.identity);

                Creator creator = _townHallPrefab.GetComponent<Creator>();
                creator.SetFlag(_flag);
                creator.SetResourceSpawner(_resourceSpawner);

                RobotWarehouse robotWarehouseThis = transform.GetComponent<RobotWarehouse>();
                Robot robot = robotWarehouseThis.Robots[robotWarehouseThis.Robots.Count - 1];
                robot.TownHallMover.ReassignTownHall(creator.transform);

                RobotWarehouse robotWarehouse = creator.GetComponent<RobotWarehouse>();
                robotWarehouse.Clear();
                robotWarehouse.AddRobot(robot);

                _resourceSpawner.GetComponent<TaskDistributor>().AddRobotWarehouse(creator.GetComponent<RobotWarehouse>());
                robotWarehouseThis.Remove(robot);

                _flag.gameObject.SetActive(false);
                _flag.SetHasFlagTaken(false);
            }
        }
    }

    private void Buy(int price)
    {
        for (int i = price - 1; i >= 0; i--)
        {
            transform.GetComponent<ResourceWarehouse>().RemoveAt(i);
        }
    }
}
