using UnityEngine;

public class PlayExample : MonoBehaviour
{
    [SerializeField] private Character _character;
    [SerializeField] private CharacterAnimator _characterAnimator;
    [SerializeField] private HealthBar _characterHealthBar;

    private void Awake()
    {
        _character.Initiate();
        _characterHealthBar.Initiate(_character);
    }

    private void Update()
    {
        if (_character.IsHit)
        {
            _characterAnimator.AnimateHit();
            _characterHealthBar.RecalculateBarWidth();

            _character.ResetHitFlag();
        }
    }
}
