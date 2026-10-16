using UnityEngine;

public class TownHallMover : State
{
    private Robot _robot;
    private Transform _townHall;
    private ResourceSpawner _spawner;
    private Transform _resource;
    private float _distanceToTownHall;
    private ResourceWarehouse _resourceWarehouse;

    public TownHallMover(Robot robot, ResourceSpawner spawner, float distanceToTownHall, Transform townHall)
    {
        _robot = robot;
        _spawner = spawner;
        _distanceToTownHall = distanceToTownHall;
        _townHall = townHall;

        if (_townHall.TryGetComponent(out ResourceWarehouse resourceWarehouse))
        {
            _resourceWarehouse = resourceWarehouse;
        }
    }

    public override void Enter()
    {
        _robot.GetNavMeshAgent().SetDestination(_townHall.position);
    }

    public override void Update()
    {
        if (Vector3.Distance(_robot.transform.position, _townHall.position) <= _distanceToTownHall)
        {
            if (_resource != null && _resource.TryGetComponent(out Resource resource))
            {
                _spawner.ReturnResource(resource);
                _resourceWarehouse.AddResource(resource);
            }

            _robot.SetHasResource(false);

            _robot.StateMachine.ChangeState(_robot.RobotMover);
        }
    }

    public void SetResourcePosition(Transform resource)
    {
        _resource = resource;
    }

    public void SetSpawner(ResourceSpawner spawner)
    {
        _spawner = spawner;
    }

    public void ReassignTownHall(Transform townHall)
    {
        _townHall = townHall;

        if (_townHall != null && _townHall.TryGetComponent(out ResourceWarehouse resourceWarehouse))
        {
            _resourceWarehouse = resourceWarehouse;
        }
        else
        {
            _resourceWarehouse = null;
        }
    }
}