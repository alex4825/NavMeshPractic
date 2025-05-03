using UnityEngine;

public class Player
{
    private IMovementInput _input;
    private IMovable _mover;
    private IRotatable _rotator;

    public Player(IMovementInput input, IMovable mover, IRotatable rotator)
    {
        _input = input;
        _mover = mover;
        _rotator = rotator;
    }

    public void UpdateLogic()
    {
        _mover.UpdateMovement(_input.GetMoveDirection());
        _rotator.UpdateRotation(_input.GetMoveDirection());
    }
}
