using UnityEngine;
using UnityEngine.Audio;

public class SoundService : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _soundsMixerGroup;

    [SerializeField] private AudioClip _footSound;
    [SerializeField] private AudioClip _explosionSound;

    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;

    AudioSource _audioSource;

    private void Awake()
    {
        GameObject soundObject = new GameObject("Sound");

        soundObject.AddComponent(typeof(AudioSource));
        _audioSource = soundObject.GetComponent<AudioSource>();
        _audioSource.outputAudioMixerGroup = _soundsMixerGroup;
    }

    public void PlayFootSound(Vector3 position) => PlayOneSound(_footSound, position);

    public void PlayExplosionSound(Vector3 position) => PlayOneSound(_explosionSound, position);

    private void PlayOneSound(AudioClip clip, Vector3 position)
    {
        _audioSource.transform.position = position;

        _audioSource.PlayOneShot(clip);
    }
}
