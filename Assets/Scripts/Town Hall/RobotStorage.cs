using System.Collections.Generic;
using UnityEngine;

public class RobotStorage : MonoBehaviour
{
    [SerializeField] private List<Robot> _robots;

    public IReadOnlyList<Robot> Robots => _robots;

    public void AddTask(int index, Vector3 resource)
    {
        _robots[index].Move(resource);
    }

    public void AddRobot(Robot robot)
    {
        _robots.Add(robot);
    }

    public void RemoveRobot(int index)
    {
        _robots.RemoveAt(index);
    }

    public void Clear()
    {
        _robots.Clear();
    }
}
