using UnityEngine;
using UnityEngine.UI;

public class IconSwitcher : MonoBehaviour
{
    [SerializeField] private Image _iconImage;

    [SerializeField] private Sprite _otherIconSprite;

    private Sprite _mainIconSprite;

    private void Awake()
    {
        _mainIconSprite = _iconImage.sprite;
    }

    public void SwitchIcon()
    {
        if (_iconImage.sprite == _mainIconSprite)
            _iconImage.sprite = _otherIconSprite; 
        else
            _iconImage.sprite = _mainIconSprite;
    }
}
