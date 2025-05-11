using UnityEngine;

public interface IAgentMovable
{
    Vector3 CurrentVelocity { get; }

    void SetDestination(Vector3 point);
}
