using UnityEngine;

public class RobotToucher : MonoBehaviour
{
    [SerializeField] private Transform _hand;
    [SerializeField] private Robot _robot;

    public Resource Resource { get; private set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent(out Resource resource) && _robot.IsBusy == false)
        {
            Resource = resource;

            ConfigureResourceForChild(resource);

            _robot.SetBusy(true);
        }
    }

    private void ConfigureResourceForChild(Resource resource)
    {
        resource.transform.SetParent(_hand, true);
        resource.transform.localPosition = Vector3.zero;
        resource.transform.localRotation = Quaternion.identity;
        resource.SetColliderEnabled(false);
        resource.SetNavMeshObstacleEnambled(false);
    }
}
