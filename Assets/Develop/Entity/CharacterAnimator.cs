using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private SoundService _soundService;
    [SerializeField] private float _transitionDuration;
    [SerializeField] private float _damageEffectDuration;

    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    private readonly int HitKey = Animator.StringToHash("Hit");
    private readonly int InJumpProcessKey = Animator.StringToHash("InJumpProcess");
    private readonly int DieKey = Animator.StringToHash("Die");
    private readonly int AliveKey = Animator.StringToHash("Alive");

    private const string InjuryLayerName = "Injury Layer";
    private const float MaxInjuryWeight = 1f;

    private DamageEffectView _damageEffectView;

    private bool IsCharacterRunning => _character.CurrentVelocity != Vector3.zero;

    private void Awake()
    {
        _animator.SetBool(IsRunningKey, false);
        _damageEffectView = new DamageEffectView(GetComponentsInChildren<Renderer>(), _damageEffectDuration, this);
    }

    private void Update()
    {
        _animator.SetBool(IsRunningKey, IsCharacterRunning);

        if (_character.IsStrongInjury)
            SetInjuryWeight(MaxInjuryWeight);
        else
            SetInjuryWeight(0);

        if (_character.IsHit)
        {
            _damageEffectView.PlayEffect();

            if (_character.IsAlive == false)
            {
                _animator.SetTrigger(DieKey);
                return;
            }

            AnimateHit();
        }

        _animator.SetBool(InJumpProcessKey, _character.InJumpProcess);
    }

    public void AnimateHit() => _animator.SetTrigger(HitKey);

    public void PlayFootSound() => _soundService.PlayFootSound(_character.Position);

    private void SetInjuryWeight(float value)
    {
        float step = _transitionDuration * Time.deltaTime;
        int injuryIndex = _animator.GetLayerIndex(InjuryLayerName);
        float currentWeight = _animator.GetLayerWeight(injuryIndex);

        _animator.SetLayerWeight(injuryIndex, Mathf.Lerp(currentWeight, value, step));
    }
}
