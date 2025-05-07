using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class Character : MonoBehaviour, IDamagable
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotationSpeed;

    private const float InjuryKoef = 0.3f;

    private IMovable _mover;
    private IRotatable _rotator;

    [SerializeField] private float _health;
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

    public bool IsReadyToGo { get; private set; }

    public bool IsStrongInjury => Health < _maxHealth * InjuryKoef;

    public void Initiate()
    {
        _mover = new ClickPointAgentMover(GetComponent<NavMeshAgent>(), _moveSpeed);
        _rotator = new DirectionalRotator(transform, _rotationSpeed);

        Health = _maxHealth;
        State = CharacterStates.Idle;

        IsReadyToGo = true;
    }

    public void Update()
    {
        if (IsReadyToGo)
        {
            _mover.UpdateMovement();
            _rotator.UpdateRotation(_mover.Velocity);

            if (_mover.Velocity == Vector3.zero)
                State = CharacterStates.Idle;
            else
                State = CharacterStates.Running;
        }
    }

    public void TakeDamage(float damage)
    {
        IsHit = true;
        _health -= damage;
    }
}
