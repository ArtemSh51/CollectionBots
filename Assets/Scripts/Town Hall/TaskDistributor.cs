using System.Collections.Generic;
using UnityEngine;

public class TaskDistributor : MonoBehaviour
{
    [SerializeField] private ResourceScanner _resourceScanner;
    [SerializeField] private List<RobotWarehouse> _robotWarehouses;

    private void Update()
    {
        int j = 0;

        for (int i = 0; i < _robotWarehouses.Count; i++)
        {
            int robotsCount = _robotWarehouses[i].Robots.Count;

            for (int k = 0; k < robotsCount && j < _resourceScanner.ScannedResources.Count; k++, j++)
            {
                _robotWarehouses[i].AddTask(k, _resourceScanner.GetResourcePosition(j));
            }
        }
    }

    public void AddRobotWarehouse(RobotWarehouse robotWarehouse)
    {
        if (robotWarehouse == null)
            return;

        if (!_robotWarehouses.Contains(robotWarehouse))
            _robotWarehouses.Add(robotWarehouse);
    }

    public void RemoveRobotWarehouse(RobotWarehouse robotWarehouse)
    {
        if (robotWarehouse == null)
            return;

        _robotWarehouses.Remove(robotWarehouse);
    }
}