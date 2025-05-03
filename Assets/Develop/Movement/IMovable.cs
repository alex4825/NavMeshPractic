using UnityEngine;

public interface IMovable
{
    float MoveSpeed { get; }

    void UpdateMovement(Vector3 direction);
}
