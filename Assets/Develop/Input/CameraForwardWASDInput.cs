using UnityEngine;

public class CameraForwardWASDInput : IMovementInput
{
    private const string HorizontalAxisName = "Horizontal";
    private const string VerticalAxisName = "Vertical";

    public Vector3 GetMoveDirection()
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
        return new Vector2(Input.GetAxisRaw(HorizontalAxisName), Input.GetAxisRaw(VerticalAxisName));
    }
}
