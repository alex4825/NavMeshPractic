using UnityEngine;

public class CharacterControllerMover : IMovable
{
    private CharacterController _controller;

    public CharacterControllerMover(CharacterController controller, float speed)
    {
        _controller = controller;
        MoveSpeed = speed;
    }

    public float MoveSpeed { get; private set; }

    public void UpdateMovement(Vector3 direction)
    {
        _controller.Move(direction * MoveSpeed * Time.deltaTime);
    }
}
