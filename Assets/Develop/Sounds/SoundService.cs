using UnityEngine;
using UnityEngine.Audio;

public class SoundService : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup _soundsMixerGroup;

    [SerializeField] private AudioClip _footSound;
    [SerializeField] private AudioClip _explosionSound;

    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;

    public void PlayFootSound(Vector3 position) => PlayOneSound(_footSound, position);

    public void PlayExplosionSound(Vector3 position) => PlayOneSound(_explosionSound, position);

    private void PlayOneSound(AudioClip clip, Vector3 position)
    {
        GameObject soundObject = new GameObject("Sound");
        soundObject.transform.position = position;

        soundObject.AddComponent(typeof(AudioSource));
        AudioSource audioSource = soundObject.GetComponent<AudioSource>();

        audioSource.outputAudioMixerGroup = _soundsMixerGroup;

        audioSource.PlayOneShot(clip);

        Destroy(soundObject, clip.length);
    }
}
