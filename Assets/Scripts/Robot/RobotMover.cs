using UnityEngine;

public class RobotMover : State
{
    private Robot _robot;
    private Transform _resource;
    private Flag _flag;
    private float _distanceToResource;

    public RobotMover(Robot robot, float distanceToResource, Flag flag)
    {
        _robot = robot;
        _flag = flag;
        _distanceToResource = distanceToResource;
    }

    public void SetResourcePosition(Transform resource)
    {
        _resource = resource;
    }

    public override void Enter()
    {
        _robot.GetNavMeshAgent().SetDestination(_resource.position);

        _flag.SetHasFlagTaken(false);
    }

    public override void Update()
    {
        if (_flag.gameObject.activeSelf)
        {
            _robot.StateMachine.ChangeState(_robot.FlagMover);
        }
        else if (Vector3.Distance(_resource.position, _robot.transform.position) <= _distanceToResource)
        {
            _robot.RobotToucher.SetResourcePosition(_resource);
            _robot.StateMachine.ChangeState(_robot.RobotToucher);
        }
    }
}
