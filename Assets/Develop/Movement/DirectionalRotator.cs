using UnityEngine;

public class DirectionalRotator : IRotatable
{
    private const float DeathZone = 0.05f;

    private Transform _transform;
    private float _rotationSpeed;

    public DirectionalRotator(Transform transform, float rotationSpeed)
    {
        _transform = transform;
        _rotationSpeed = rotationSpeed;
    }

    public void UpdateRotation(Vector3 currentVelocity)
    {
        if (currentVelocity.magnitude < DeathZone)
            return;

        Quaternion aimRotation = Quaternion.LookRotation(currentVelocity);
        float step = _rotationSpeed * Time.deltaTime;
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, aimRotation, step);
    }
}
