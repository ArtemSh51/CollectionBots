using UnityEngine;

public class FlagMover : State
{
    private Robot _robot;
    private Flag _flag;
    private float _distanceToFlag;

    public FlagMover(Robot robot, float distanceToFlag)
    {
        _robot = robot;
        _distanceToFlag = distanceToFlag;
    }

    public override void Enter()
    {
        _robot.GetNavMeshAgent().SetDestination(_flag.transform.position);
    }

    public override void Update()
    {
        if (Vector3.Distance(_robot.transform.position, _flag.transform.position) <= _distanceToFlag)
        {
            _robot.SetHasResource(false);

            _robot.StateMachine.ChangeState(_robot.RobotMover);

            _flag.SetHasFlagTaken(true);
            _flag.Deactivate();

        }
    }

    public void SetFlagTransform(Flag flag)
    {
        _flag = flag;
    }
}
