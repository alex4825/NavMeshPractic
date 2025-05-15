using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private float _transitionSpeed;

    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    private readonly int HitKey = Animator.StringToHash("Hit");
    private readonly int InJumpProcessKey = Animator.StringToHash("InJumpProcess");

    private readonly string InjuryLayerName = "Injury Layer";
    private const float MaxInjuryWeight = 1f;

    private bool IsCharacterRunning => _character.CurrentVelocity != Vector3.zero;

    private void Awake()
    {
        _animator.SetBool(IsRunningKey, false);
    }

    private void Update()
    {
        _animator.SetBool(IsRunningKey, IsCharacterRunning);

        if (_character.IsStrongInjury)
            SetInjuryWeight(MaxInjuryWeight);
        else
            SetInjuryWeight(0);

        if (_character.IsHit)
            AnimateHit();

        _animator.SetBool(InJumpProcessKey, _character.InJumpProcess);
    }

    public void AnimateHit() => _animator.SetTrigger(HitKey);

    private void SetInjuryWeight(float value)
    {
        float step = _transitionSpeed * Time.deltaTime;
        int injuryIndex = _animator.GetLayerIndex(InjuryLayerName);
        float currentWeight = _animator.GetLayerWeight(injuryIndex);

        _animator.SetLayerWeight(injuryIndex, Mathf.Lerp(currentWeight, value, step));
    }
}
