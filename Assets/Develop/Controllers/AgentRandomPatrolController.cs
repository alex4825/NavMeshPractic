using UnityEngine;

public class AgentRandomPatrolController : Controller
{
    private IAgentMovable _movable;
    private float _patrolRadius;

    private Vector3 _currentDestination;

    public AgentRandomPatrolController(IAgentMovable movable, float patrolRadius)
    {
        _movable = movable;
        _patrolRadius = patrolRadius;
    }

    public override bool HasInput => _currentDestination == _movable.CurrentDestination && _movable.CurrentVelocity != Vector3.zero;

    protected override void UpdateLogic()
    {
        if (_movable.CurrentVelocity == Vector3.zero)
        {
            _movable.SetDestination(GetRandomDestinationInRadius());
            _currentDestination = _movable.CurrentDestination;
        }
    }

    private Vector3 GetRandomDestinationInRadius()
    {
        float randomAngle = Random.Range(0, 360f);
        float randomDirectionLength = Random.Range(0, _patrolRadius);
        Vector3 moveDirectionNormalized = (Quaternion.Euler(0f, randomAngle, 0f) * Vector3.forward).normalized;

        return moveDirectionNormalized * randomDirectionLength;
    }
}
