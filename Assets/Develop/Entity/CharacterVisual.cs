using UnityEngine;

public class CharacterVisual : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Character _character;

    private readonly int IsRunningKey = Animator.StringToHash("IsRunning");
    private readonly int HitKey = Animator.StringToHash("Hit");
    private readonly int IsDieKey = Animator.StringToHash("IsDie");

    private CharacterStates _characterLastFrameState;

    private void Awake()
    {
        UpdateLastFrameState(); 
        SetAnimationFrom(_character.State);
    }

    private void Update()
    {
        if (_characterLastFrameState != _character.State)
        {
            SetAnimationFrom(_character.State);
        }

        UpdateLastFrameState();

        if (_character.IsHit)
        {
            _animator.SetTrigger(HitKey);
        }

    }
    
    private void UpdateLastFrameState() => _characterLastFrameState = _character.State;

    private void SetAnimationFrom(CharacterStates state)
    {
        switch (state)
        {
            case CharacterStates.Idle:
                _animator.SetBool(IsRunningKey, false);
                break;
            
            case CharacterStates.Running:
                _animator.SetBool(IsRunningKey, true);
                break;
            
            case CharacterStates.Die:
                _animator.SetBool(IsDieKey, false);
                break;
        }
    }
}
