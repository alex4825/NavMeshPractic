using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour, IDamagable, IHealthable, IAgentMovable
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private HealthBar _healthBar;

    private AgentMover _mover;
    private DirectionalRotator _rotator;

    private const float InjuryKoef = 0.3f;
    private float _health;

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

    public Vector3 CurrentDestination => _agent.destination;

    public void Awake()
    {
        _agent.updateRotation = false;

        _mover = new AgentMover(_agent, _moveSpeed);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);

        Health = MaxHealth;
        _healthBar.Initiate(this);
    }

    public void Update()
    {
        _rotator.SetDirection(_mover.CurrentVelocity);
        _rotator.Update();
    }

    private void LateUpdate()
    {
        ResetHitFlag();
    }

    public void SetDestination(Vector3 point) => _mover.SetDestination(point);

    public void TakeDamage(float damage)
    {
        IsHit = true;

        if (damage > 0)
            Health -= damage;
    }

    private void ResetHitFlag() => IsHit = false;
}
