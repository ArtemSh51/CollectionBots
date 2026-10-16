using System.Collections.Generic;
using UnityEngine;

public class RobotWarehouse : MonoBehaviour
{
    [SerializeField] private List<Robot> _robots;

    public IReadOnlyList<Robot> Robots => _robots;

    public int Count => _robots.Count;

    public void AddTask(int index, Transform target)
    {
        _robots[index].SetResorcePosition(target);
    }
    public void AddRobot(Robot robot)
    {
        _robots.Add(robot);
    }

    public void Remove(Robot robot)
    {
        _robots.Remove(robot);
    }

    public void Clear()
    {
        _robots.Clear();
    }
}
