using UnityEngine;

public class AgentClickPointController : Controller
{
    private const int LeftMouseKey = 0;

    private IAgentMovable _movable;
    private LayerMask _groundMask;

    private bool _isDestinationSetted;

    public AgentClickPointController(IAgentMovable movable, LayerMask groundMask)
    {
        _movable = movable;
        _groundMask = groundMask;
    }

    public override bool HasInput => _isDestinationSetted;

    protected override void UpdateLogic()
    {
        _isDestinationSetted = TryGetClickPosition(out Vector3 clickPosition);

        if (_isDestinationSetted)
        {
            _movable.SetDestination(clickPosition);
        }
    }

    private bool TryGetClickPosition(out Vector3 clickPosition)
    {
        if (Input.GetMouseButtonDown(LeftMouseKey))
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
