using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour, IDamagable, IAgentMovable
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private HealthBar _healthBar;

    [SerializeField] private float _patrolRadius;

    private AgentMover _mover;
    private DirectionalRotator _rotator;

    private const float InjuryKoef = 0.3f;
    private float _health;

    public CharacterStates State { get; private set; }

    public bool IsHit { get; private set; }

    [field: SerializeField] public float MaxHealth { get; private set; }

    public float Health
    {
        get { return _health; }

        private set { _health = value >= 0 ? value : 0; }
    }

    public bool IsStrongInjury => Health < MaxHealth * InjuryKoef;

    public Vector3 Position => transform.position;

    public Vector3 CurrentVelocity => _mover.CurrentVelocity;

    public void Awake()
    {
        _agent.updateRotation = false;

        _mover = new AgentMover(_agent, _moveSpeed);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);

        Health = MaxHealth;
        _healthBar.Initiate(this);
        State = CharacterStates.Idle;
    }

    public void Update()
    {
        _rotator.SetDirection(_mover.CurrentVelocity);
        _rotator.Update();

        if (_mover.CurrentVelocity == Vector3.zero)
            State = CharacterStates.Idle;
        else
            State = CharacterStates.Running;
    }

    private void LateUpdate()
    {
        ResetHitFlag();
    }

    public void SetDestination(Vector3 point) => _mover.SetDestination(point);

    public void TakeDamage(float damage)
    {
        IsHit = true;
        Health -= damage;
    }

    private void ResetHitFlag() => IsHit = false;

}
