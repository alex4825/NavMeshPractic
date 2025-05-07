using UnityEngine;

public class DirectionalMover
{
    private CharacterController _controller;
    private float _moveSpeed;
    private Vector3 _currentDirection;

    public DirectionalMover(CharacterController controller, float speed)
    {
        _controller = controller;
        _moveSpeed = speed;
    }

    public Vector3 CurrentVelocity { get; private set; }

    public void SetDirection(Vector3 direction) => _currentDirection = direction;

    public void UpdateMovement()
    {
        CurrentVelocity = _currentDirection.normalized * _moveSpeed;

        _controller.Move(CurrentVelocity * Time.deltaTime);
    }
}
