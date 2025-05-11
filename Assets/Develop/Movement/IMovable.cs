using UnityEngine;

public interface IMovable
{
    bool IsEnable { get; set; }

    Vector3 Velocity { get; }

    void UpdateMovement();
}
