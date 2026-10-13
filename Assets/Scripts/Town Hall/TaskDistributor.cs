using System.Linq;
using UnityEngine;

public class TaskDistributor : MonoBehaviour
{
    [SerializeField] private RobotStorage _robotStorage;
    [SerializeField] private ResourceScanner _resourceScanner;

    private void Update()
    {
        for (int i = 0; i < _resourceScanner.Resources.Count(); i++)
        {
            if (i == _resourceScanner.Resources.Count() - (_resourceScanner.Resources.Count() - _robotStorage.Robots.Count()))
            {
                break;
            }

            if (_robotStorage.Robots[i].IsBusy == false)
            {
                _robotStorage.AddTask(i, _resourceScanner.GetPosition(i));
            }
        }
    }
}
