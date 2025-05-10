using UnityEngine;
using UnityEngine.AI;

public class AgentDestinationDrawer : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private GameObject _pointerPrefab;
    [SerializeField] private Vector3 _pointerOffset;
    [SerializeField] private float _pathCornerRadius;

    private GameObject _pointer;

    private void Awake()
    {
        _pointer = Instantiate(_pointerPrefab);
    }

    private void Update()
    {
        _pointer.SetActive(_agent.hasPath);
        _pointer.transform.position = _agent.destination + _pointerOffset;
    }

    private void OnDrawGizmos()
    {
        if (_agent.hasPath)
        {
            foreach (Vector3 corner in _agent.path.corners)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(corner, _pathCornerRadius);
            }
        }
    }

}
