using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Collider), typeof(NavMeshObstacle))]
public class Resource : MonoBehaviour
{
    private Collider _collider;
    private NavMeshObstacle _navMeshObstacle;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _navMeshObstacle = GetComponent<NavMeshObstacle>();
    }

    public void SetColliderEnabled(bool isEnableCollider)
    {
        _collider.enabled = isEnableCollider;
    }

    public void SetNavMeshObstacleEnambled(bool isEnableNavMeshObstacle)
    {
        _navMeshObstacle.enabled = isEnableNavMeshObstacle;
    }
}
