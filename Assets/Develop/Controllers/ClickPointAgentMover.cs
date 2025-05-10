using UnityEngine;
using UnityEngine.AI;

public class ClickPointAgentMover : IMovable
{
    private NavMeshAgent _agent;
    private float _moveSpeed;
    private LayerMask _groundMask;

    public ClickPointAgentMover(NavMeshAgent agent, float moveSpeed, LayerMask groundMask)
    {
        _agent = agent;
        _moveSpeed = moveSpeed;
        _agent.speed = _moveSpeed;
        _groundMask = groundMask;

        _agent.updateRotation = false;
    }

    public Vector3 Velocity => _agent.desiredVelocity;

    public void UpdateMovement()
    {
        if (TryGetClickPosition(out Vector3 clickPosition))
            _agent.SetDestination(clickPosition);
    }

    private bool TryGetClickPosition(out Vector3 clickPosition)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cursorRay, out RaycastHit hit, Mathf.Infinity, _groundMask))
            {
                clickPosition = hit.point;
                return true;
            }
        }

        clickPosition = Vector3.zero;
        return false;
    }
}
