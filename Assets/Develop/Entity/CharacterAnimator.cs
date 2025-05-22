using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private SoundService _soundService;
    [SerializeField] private float _transitionDuration;
    [SerializeField] private float _damageEffectDuration;
    [SerializeField] private float _dissolveEffectDuration;

    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    private readonly int HitKey = Animator.StringToHash("Hit");
    private readonly int InJumpProcessKey = Animator.StringToHash("InJumpProcess");
    private readonly int DieKey = Animator.StringToHash("Die");
    private readonly int AliveKey = Animator.StringToHash("Alive");

    private const string InjuryLayerName = "Injury Layer";
    private const float MaxInjuryWeight = 1f;

    private const string DamageStranghtKey = "_DamageStranght";
    private const string DissolveAdgeKey = "_DissolveAdge";

    private ShortEffectView _shortEffectView;

    private bool _isCharacterDie;

    private bool IsCharacterRunning => _character.CurrentVelocity != Vector3.zero;

    private void Awake()
    {
        _animator.SetBool(IsRunningKey, false);
        _shortEffectView = new ShortEffectView(GetComponentsInChildren<Renderer>(), this);
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
            _shortEffectView.PlayIncreaseDecreaseEffect(DamageStranghtKey, _damageEffectDuration);

            if (_character.IsAlive == false)
            {
                _isCharacterDie = true;
                _animator.SetTrigger(DieKey);
                _shortEffectView.PlayIncreaseEffect(DissolveAdgeKey, _dissolveEffectDuration);
                return;
            }

            AnimateHit();
        }

        if (_isCharacterDie && _character.IsAlive)
        {
            _isCharacterDie = false;
            _animator.SetTrigger(AliveKey);
            _shortEffectView.PlayDecreaseEffect(DissolveAdgeKey, _dissolveEffectDuration);
        }

        _animator.SetBool(InJumpProcessKey, _character.InJumpProcess);
    }

    public void AnimateHit() => _animator.SetTrigger(HitKey);

    public void PlayFootSound() => _soundService.PlayFootSound(GetComponent<AudioSource>());

    private void SetInjuryWeight(float value)
    {
        float step = _transitionDuration * Time.deltaTime;
        int injuryIndex = _animator.GetLayerIndex(InjuryLayerName);
        float currentWeight = _animator.GetLayerWeight(injuryIndex);

        _animator.SetLayerWeight(injuryIndex, Mathf.Lerp(currentWeight, value, step));
    }
}
