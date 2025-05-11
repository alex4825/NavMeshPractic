using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;

public class PlayExample : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _maxIdleTime;
    [SerializeField] private float _patrolRadius;

    private Controller _agentIdleController;
    private Controller _agentDefaultController;

    private float _idleTimer;

    private void Awake()
    {
        _agentDefaultController = new AgentClickPointController(_character, _groundMask);
        _agentIdleController = new AgentRandomPatrolController(_character, _patrolRadius);

        _agentDefaultController.IsEnabled = true;
    }

    private void Update()
    {
        switch (_character.State)
        {
            case CharacterStates.Idle:
                _idleTimer += Time.deltaTime;
                break;

            case CharacterStates.Running:
                _idleTimer = 0;

                if (_agentDefaultController.HasInput)
                    _agentIdleController.IsEnabled = false;

                break;

        }

        if (_idleTimer >= _maxIdleTime)
        {
            _idleTimer = 0;
            _agentIdleController.IsEnabled = true;
        }

        _agentDefaultController.Update();
        _agentIdleController.Update();
    }
}
