using System.Collections;
using UnityEngine;

public class DamageEffectView
{
    private const string EffectKey = "_EffectStrangth";

    private Renderer[] _renderers;
    private MonoBehaviour _monoBehaviour;

    private float _duration;
    private float _timer;

    public DamageEffectView(Renderer[] renderers, float duration, MonoBehaviour monoBehaviour)
    {
        _renderers = renderers;
        _duration = duration;
        _monoBehaviour = monoBehaviour;
    }

    public void PlayEffect() => _monoBehaviour.StartCoroutine(EffectProcess());

    private IEnumerator EffectProcess()
    {
        float durationHalf = _duration / 2;

        while (_timer <= durationHalf)
        {
            SetEffect(_timer / durationHalf);
            _timer += Time.deltaTime;
            yield return null;
        }

        while (_timer >= 0)
        {
            SetEffect(_timer / durationHalf);
            _timer -= Time.deltaTime;
            yield return null;
        }

        _timer = 0;
    }

    private void SetEffect(float value)
    {
        foreach (Renderer renderer in _renderers)
            renderer.material.SetFloat(EffectKey, value);
    }
}
