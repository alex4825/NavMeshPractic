using UnityEngine;

public class DetonationView
{
    private const string BlinkSpeedKey = "_BlinkSpeed";

    private Renderer _meshRenderer;
    private float _detonationSpeed;

    public DetonationView(Renderer meshRenderer, float detonationSpeed)
    {
        _meshRenderer = meshRenderer;
        _detonationSpeed = detonationSpeed;
    }

    public void Play() => _meshRenderer.material.SetFloat(BlinkSpeedKey, _detonationSpeed);
}
