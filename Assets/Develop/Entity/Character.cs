using UnityEngine;

public class Character : MonoBehaviour, IDamagable
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private IMovementInput _input;
    private IMovable _mover;
    private IRotatable _rotator;
    private float _health;
    private bool _isHit;

    public CharacterStates State { get; private set; }

    public bool IsHit
    {
        get
        {
            if (_isHit)
            {
                _isHit = false;
                return true;
            }

            return _isHit;
        }

        private set { _isHit = value; }
    }

    public float Health
    {
        get { return _health; }

        private set { _health = value >= 0 ? value : 0; }
    }

    private void Awake()
    {
        _input = new CameraForwardWASDInput();
        _mover = new CharacterControllerMover(GetComponent<CharacterController>(), _moveSpeed);
        _rotator = new TransformRotator(transform, _rotationSpeed);

        Health = _maxHealth;
        State = CharacterStates.Idle;
    }

    public void Update()
    {
        Vector3 moveDirection = _input.GetMoveDirection();

        if (moveDirection == Vector3.zero)
        {
            State = CharacterStates.Idle;
        }
        else
        {
            State = CharacterStates.Running;

            _mover.UpdateMovement(moveDirection);
            _rotator.UpdateRotation(moveDirection);
        }
    }

    public void TakeDamage(float damage)
    {
        IsHit = true;
        _health -= damage;
    }
}
