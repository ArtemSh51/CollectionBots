using UnityEngine;

public class RobotToucher : State
{
    private Robot _robot;
    private Transform _hand;
    private Transform _resource;

    public RobotToucher(Robot robot, Transform hand)
    {
        _robot = robot;
        _hand = hand;
    }

    public void SetResourcePosition(Transform resource)
    {
        _resource = resource;
    }

    public override void Enter()
    {
        _resource.transform.SetParent(_hand, true);
        _resource.transform.localPosition = Vector3.zero;
        _resource.transform.localRotation = Quaternion.identity;
        
        if (_resource.TryGetComponent(out Resource resource))
        {
            resource.SetColliderEnabled(false);
            resource.SetNavMeshObstacleEnabled(false);
        }

        _robot.TownHallMover.SetResourcePosition(_resource);
        _robot.StateMachine.ChangeState(_robot.TownHallMover);
    }
}
