using UnityEngine;

public class WASDCharacterControllerMover : IMovable
{
    private const string HorizontalAxisKey = "Horizontal";
    private const string VerticalAxisKey = "Vertical";

    private CharacterController _controller;
    private float _moveSpeed;

    public WASDCharacterControllerMover(CharacterController controller, float speed)
    {
        _controller = controller;
        _moveSpeed = speed;
    }

    public Vector3 Velocity { get; private set; }

    public void UpdateMovement()
    {
        Velocity = GetMoveDirectionNormalized() * _moveSpeed;

        _controller.Move(Velocity * Time.deltaTime);
    }

    private Vector3 GetMoveDirectionNormalized()
        => new Vector3(Input.GetAxisRaw(HorizontalAxisKey), 0, Input.GetAxisRaw(VerticalAxisKey)).normalized;
}
