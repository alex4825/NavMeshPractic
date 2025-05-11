using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour, IDamagable
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _patrolRadius;
    [SerializeField] private float _maxIdleTime;

    private const float InjuryKoef = 0.3f;
    private float _health;
    private float _idleTimer;

    private IMovable _mover;
    private IMovable _idleMover;
    private IRotatable _rotator;

    [field: SerializeField]
    public float MaxHealth { get; private set; }

    public CharacterStates State { get; private set; }

    public bool IsHit { get; private set; }

    public float Health
    {
        get { return _health; }

        private set { _health = value >= 0 ? value : 0; }
    }

    public bool IsStrongInjury => Health < MaxHealth * InjuryKoef;

    public Vector3 Position => transform.position;

    public void Initiate()
    {
        _idleMover = new RandomPatrolAgentMover(GetComponent<NavMeshAgent>(), _moveSpeed, _patrolRadius);
        _mover = new ClickPointAgentMover(GetComponent<NavMeshAgent>(), _moveSpeed, _groundMask);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);

        Health = MaxHealth;
        State = CharacterStates.Idle;

        _mover.IsEnable = true;
        _idleMover.IsEnable = false;
    }

    public void Update()
    {
        _mover.UpdateMovement();
        _idleMover.UpdateMovement();
        _rotator.UpdateRotation(_mover.Velocity);

        if (_mover.IsEnable)
            if (_mover.Velocity == Vector3.zero)
            {
                State = CharacterStates.Idle;
                _idleTimer += Time.deltaTime;
            }
            else
            {
                State = CharacterStates.Running;
                _idleTimer = 0;
            }

        if (_idleTimer >= _maxIdleTime)
        {
            _mover.IsEnable = false;
            _idleMover.IsEnable = true;
        }
    }

    public void TakeDamage(float damage)
    {
        IsHit = true;
        Health -= damage;
    }

    public void ResetHitFlag() => IsHit = false;

    public void EnableAutoMovement()
    {

    }
}
