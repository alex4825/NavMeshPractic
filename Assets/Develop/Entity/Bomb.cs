using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private SphereCollider _triggerCollider;
    [SerializeField] private ParticleSystem _explosionParticlesPrefab;
    [SerializeField] private float _delayAfterTrigger;
    [SerializeField] private float _damage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamagable>(out IDamagable damagable))
            StartCoroutine(ExplodeProcess(damagable));
    }

    private IEnumerator ExplodeProcess(IDamagable damagable)
    {
        yield return new WaitForSeconds(_delayAfterTrigger);

        if (InTriggerZone(damagable))
            damagable.TakeDamage(_damage);

        Instantiate(_explosionParticlesPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, _triggerCollider.radius);
    }

    private bool InTriggerZone(IDamagable damagable) => Vector3.Distance(damagable.Position, transform.position) < _triggerCollider.radius;
}
