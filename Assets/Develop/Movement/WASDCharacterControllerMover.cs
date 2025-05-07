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
        Velocity = GetMoveDirectionNormalized().normalized * _moveSpeed;

        _controller.Move(Velocity * Time.deltaTime);
    }

    private Vector3 GetMoveDirectionNormalized()
    {
        Vector2 inputVector = GetWASDDirection();

        Vector3 forwardDirection = GetXZProectionOf(Camera.main.transform.forward) * inputVector.y;
        Vector3 rightDirection = Camera.main.transform.right * inputVector.x;

        return (forwardDirection + rightDirection).normalized;
    }

    private Vector3 GetXZProectionOf(Vector3 vector)
        => new Vector3(vector.x, 0, vector.z).normalized;

    private Vector2 GetWASDDirection()
    {
        return new Vector2(Input.GetAxisRaw(HorizontalAxisKey), Input.GetAxisRaw(VerticalAxisKey));
    }
}
