using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider), typeof(NavMeshObstacle))]
public class Resource : MonoBehaviour
{
    private Collider _collider;
    private NavMeshObstacle _obstacle;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _obstacle = GetComponent<NavMeshObstacle>();
    }

    public void SetColliderEnabled(bool isEnable)
    {
        _collider.enabled = isEnable;
    }

    public void SetNavMeshObstacleEnabled(bool isEnable)
    {
        _obstacle.enabled = isEnable;
    }
}
