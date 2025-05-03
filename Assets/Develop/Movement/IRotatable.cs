using UnityEngine;

public interface IRotatable
{
    float RotationSpeed { get; }

    void UpdateRotation(Vector3 direction);
}
