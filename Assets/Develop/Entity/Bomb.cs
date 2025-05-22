using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private SphereCollider _triggerCollider;
    [SerializeField] private ParticleSystem _explosionParticlesPrefab;

    [SerializeField] private SoundService _soundService;

    [SerializeField] private float _delayAfterTrigger;
    [SerializeField] private float _damage;
    [SerializeField] private float _detonationSpeed;

    private DetonationView _detonationView;

    private void Awake()
    {
        _detonationView = new DetonationView(GetComponent<Renderer>(), _detonationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<IDamagable>(out IDamagable damagable))
            StartCoroutine(ExplodeProcess(damagable));
    }

    private IEnumerator ExplodeProcess(IDamagable damagable)
    {
        _detonationView.Play();

        yield return new WaitForSeconds(_delayAfterTrigger);

        if (InTriggerZone(damagable))
            damagable.TakeDamage(_damage);

        Instantiate(_explosionParticlesPrefab, transform.position, Quaternion.identity);

        _soundService.PlayExplosionSound(GetComponent<AudioSource>());

        GetComponent<Renderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, _soundService.ExplosionSoundLength);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, _triggerCollider.radius);
    }

    private bool InTriggerZone(IDamagable damagable) => Vector3.Distance(damagable.Position, transform.position) < _triggerCollider.radius;
}
