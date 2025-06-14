using System;
using UnityEngine;
using UnityEngine.AI;

public class AgentCharacter : MonoBehaviour, IDamagable, IHealthable, IAgentMovable
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private HealthBar _healthBar;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    [SerializeField] private float _jumpSpeed;
    [SerializeField] private AnimationCurve _jumpCurve;

    [SerializeField] private float _deadDuration;

    private AgentMover _mover;
    private DirectionalRotator _rotator;
    private AgentJumper _jumper;

    private const float InjuryKoef = 0.3f;
    private float _health;
    private bool _isInDesroyProcess;

    public event Action<AgentCharacter, float> Dead;
    public event Action Hit;

    public bool IsAlive => Health > 0;

    public bool IsDead => Health <= 0;

    public float Lifetime { get; private set; } = 0;

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

    public bool InJumpProcess => _jumper.InProcess;

    private void Awake()
    {
        _agent.updateRotation = false;

        _mover = new AgentMover(_agent, _moveSpeed);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);
        _jumper = new AgentJumper(_jumpSpeed, _agent, this, _jumpCurve);

        Health = MaxHealth;
        _healthBar.Initiate(this);
    }

    private void Update()
    {
        if (IsAlive)
        {
            _rotator.Update();
            Lifetime += Time.deltaTime;
        }

        if (IsDead && _isInDesroyProcess == false)
            Kill();
    }

    public void SetDestination(Vector3 point) => _mover.SetDestination(point);

    public void SetRotation(Vector3 direction) => _rotator.SetDirection(direction);

    public void TakeDamage(float damage)
    {
        if (IsDead)
            return;

        Hit?.Invoke();

        if (damage > 0)
            Health -= damage;
    }

    public bool IsOnNavMeshLink(out OffMeshLinkData offMeshLinkData)
    {
        if (_agent.isOnOffMeshLink)
        {
            offMeshLinkData = _agent.currentOffMeshLinkData;
            return true;
        }

        offMeshLinkData = default;
        return false;
    }

    public void Jump(OffMeshLinkData offMeshLinkData) => _jumper.Jump(offMeshLinkData);

    public void Kill()
    {
        Dead?.Invoke(this, _deadDuration);
        Destroy(gameObject, _deadDuration);
        _isInDesroyProcess = true;

        _healthBar.gameObject.SetActive(false);
        _mover.Stop();
    }

    public void Reborn()
    {
        Health = MaxHealth;
        Lifetime = 0;
    }
}
