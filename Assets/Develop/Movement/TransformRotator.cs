using UnityEngine;

public class TransformRotator : IRotatable
{
    private Transform _target;

    public TransformRotator(Transform target, float rotationSpeed)
    {
        _target = target;
        RotationSpeed = rotationSpeed;
    }

    public float RotationSpeed { get; private set; }

    public void UpdateRotation(Vector3 direction)
    {
        if (direction == Vector3.zero)
            return;

        Quaternion aimRotation = Quaternion.LookRotation(direction);
        float step = RotationSpeed * Time.deltaTime;
        _target.rotation = Quaternion.RotateTowards(_target.rotation, aimRotation, step);
    }
}
