using UnityEngine;

public class DirectionalRotator
{
    private const float DeathZone = 0.05f;

    private Transform _transform;
    private float _rotationSpeed;
    private Vector3 _currentDirection;

    public DirectionalRotator(Transform transform, float rotationSpeed)
    {
        _transform = transform;
        _rotationSpeed = rotationSpeed;
    }

    public Quaternion CurrentRotation => _transform.rotation;

    public void SetDirection(Vector3 direction) => _currentDirection = direction;

    public void UpdateRotation()
    {
        if (_currentDirection.magnitude < DeathZone)
            return;

        Quaternion aimRotation = Quaternion.LookRotation(_currentDirection);
        float step = _rotationSpeed * Time.deltaTime;
        _transform.rotation = Quaternion.RotateTowards(_transform.rotation, aimRotation, step);
    }
}
