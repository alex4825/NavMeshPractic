using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _barImage;

    private IDamagable _damagable;
    private float _maxWidth;
    private float _healthWidthKoef;

    private bool _isInitiated;

    public void Initiate(IDamagable damagable)
    {
        _damagable = damagable;

        _maxWidth = _barImage.rectTransform.rect.width;
        _healthWidthKoef = _maxWidth / _damagable.MaxHealth;

        _isInitiated = true;
    }

    public void RecalculateBarWidth()
    {
        if (_isInitiated) 
            _barImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, _damagable.Health * _healthWidthKoef);
    }
}