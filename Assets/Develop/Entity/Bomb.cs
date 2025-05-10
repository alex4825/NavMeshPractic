using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private SphereCollider _triggerCollider;
    [SerializeField] private ParticleSystem _explosionParticlesPrefab;
    [SerializeField] private float _delayAfterTrigger;
    [SerializeField] private float _damage;

    private float _timer = 0;
    private IDamagable _currentDamagable;

    private void Update()
    {
        if (_currentDamagable != null)
        {
            _timer += Time.deltaTime;

            if (_timer > _delayAfterTrigger)
            {
                if (IsDamagableInTriggerZone())
                    _currentDamagable.TakeDamage(_damage);

                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamagable>(out IDamagable damagable))
        {
            _currentDamagable = damagable;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, _triggerCollider.radius);
    }

    private void OnDestroy()
    {
        if (Application.isPlaying)
            Instantiate(_explosionParticlesPrefab, transform.position, Quaternion.identity);
    }

    private bool IsDamagableInTriggerZone() => Vector3.Distance(_currentDamagable.Position, transform.position) < _triggerCollider.radius;
}
