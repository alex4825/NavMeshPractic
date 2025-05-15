using UnityEngine;
using UnityEngine.Audio;

public class AudioToogler : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;

    private const string MusicKey = "MusicVolume";
    private const string SoundsKey = "SoundsVolume";

    private const float OffVolume = -80;

    private float _defaultMusicVolume;
    private float _defaultSoundsVolume;

    private void Awake()
    {
        _audioMixer.GetFloat(MusicKey, out _defaultMusicVolume);
        _audioMixer.GetFloat(SoundsKey, out _defaultSoundsVolume);
    }

    public void ToogleMusic() => ToogleAudio(MusicKey, _defaultMusicVolume);

    public void ToogleSounds() => ToogleAudio(SoundsKey, _defaultSoundsVolume);

    private void ToogleAudio(string audioKey, float defaultValue)
    {
        _audioMixer.GetFloat(audioKey, out float currentVolume);

        if (currentVolume == defaultValue)
            _audioMixer.SetFloat(audioKey, OffVolume);
        else
            _audioMixer.SetFloat(audioKey, defaultValue);
    }
}
