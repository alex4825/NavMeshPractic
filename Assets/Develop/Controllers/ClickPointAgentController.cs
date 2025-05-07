using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ClickPointAgentController : Controller
{
    private NavMeshAgent _agent;
    private IMovable _movable;
    private IRotatable _rotatable;

    public ClickPointAgentController(NavMeshAgent agent, IMovable movable, IRotatable rotatable)
    {
        _agent = agent;
        _movable = movable;
        _rotatable = rotatable;
    }

    protected override void UpdateLogic()
    {
        if (TryGetClickPosition(out Vector3 clickPosition))
        {
            _agent.SetDestination(clickPosition);
        }


    }

    private bool TryGetClickPosition(out Vector3 clickPosition)
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray cursorRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(cursorRay, out RaycastHit hit, Mathf.Infinity))
            {
                clickPosition = hit.point;
                return true;
            }
        }

        clickPosition = Vector3.zero;
        return false;
    }
}
