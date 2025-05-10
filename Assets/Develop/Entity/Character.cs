using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour, IDamagable
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private const float InjuryKoef = 0.3f;

    private IMovable _mover;
    private IRotatable _rotator;

    [SerializeField] private float _health;

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

    public void Initiate()
    {
        _mover = new ClickPointAgentMover(GetComponent<NavMeshAgent>(), _moveSpeed);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);

        Health = MaxHealth;
        State = CharacterStates.Idle;
    }

    public void Update()
    {
        _mover.UpdateMovement();
        _rotator.UpdateRotation(_mover.Velocity);

        if (_mover.Velocity == Vector3.zero)
            State = CharacterStates.Idle;
        else
            State = CharacterStates.Running;
    }

    public void TakeDamage(float damage)
    {
        IsHit = true;
        Health -= damage;
    }

    public void ResetHitFlag() => IsHit = false;
}
