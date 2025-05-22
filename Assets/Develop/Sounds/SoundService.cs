using UnityEngine;

public class SoundService : MonoBehaviour
{
    [SerializeField] private AudioClip _footSound;
    [SerializeField] private AudioClip _explosionSound;

    public float FootSoundLength => _footSound.length;

    public float ExplosionSoundLength => _explosionSound.length;

    public void PlayFootSound(AudioSource audioSource) => audioSource.PlayOneShot(_footSound);

    public void PlayExplosionSound(AudioSource audioSource) => audioSource.PlayOneShot(_explosionSound);
}
