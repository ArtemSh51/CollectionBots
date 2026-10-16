using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Robot : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private Transform _townHall;
    [SerializeField] private ResourceSpawner _spawner;
    [SerializeField] private float _distanceToResource;
    [SerializeField] private float _distanceToTownHall;
    [SerializeField] private float _distanceToFlag;
    [SerializeField] private Flag _flag;

    private Transform _resource;
    private NavMeshAgent _agent;
    private bool _hasResource;

    public StateMachine StateMachine { get; private set; }

    public RobotMover RobotMover { get; private set; }

    public RobotToucher RobotToucher { get; private set; }

    public TownHallMover TownHallMover { get; private set; }

    public FlagMover FlagMover { get; private set; }

    private void Start()
    {
        _flag.Appeared += SetFlagTransform;

        _agent = GetComponent<NavMeshAgent>();

        StateMachine = new StateMachine();
        RobotMover = new RobotMover(this, _distanceToResource, _flag);
        RobotToucher = new RobotToucher(this, _hand);
        TownHallMover = new TownHallMover(this, _spawner, _distanceToTownHall, _townHall);
        FlagMover = new FlagMover(this, _distanceToFlag);
    }

    private void Update()
    {
        StateMachine.Update();
    }

    private void OnDisable()
    {
        _flag.Appeared -= SetFlagTransform;
    }

    private void SetFlagTransform(Flag flag)
    {
        FlagMover.SetFlagTransform(flag);
    }

    public void SetResorcePosition(Transform resource)
    {
        if (RobotMover == null)
            return;

        _resource = resource;

        if (_hasResource == false)
        {
            RobotMover.SetResourcePosition(_resource);

            StateMachine.ChangeState(RobotMover);

            _hasResource = true;
        }
    }

    public void SetHasResource(bool isReceivedResources)
    {
        _hasResource = isReceivedResources;
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return _agent;
    }

    public void SetFlag(Flag flag)
    {
        _flag = flag;
    }

    public void SetResourceSpawner(ResourceSpawner resourceSpawner)
    {
        _spawner = resourceSpawner;
    }

    public void SetTownHallTransform(Transform townHall)
    {
        _townHall = townHall;
    }
}
