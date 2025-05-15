using UnityEngine;

public class CharacterSoundManager : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _footSound;

    [SerializeField] private float _minPitch;
    [SerializeField] private float _maxPitch;

    public void PlayFootSound()
    {
        _audioSource.pitch = Random.Range(_minPitch, _maxPitch);
        _audioSource.PlayOneShot(_footSound);
    }
}
