using UnityEngine;

public class PlayExample : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _maxIdleTime;
    [SerializeField] private float _patrolRadius;

    private Controller _agentCharacterController;

    private void Awake()
    {
        _agentCharacterController = new CompositeController(
            new AgentClickPointController(_character, _groundMask),
            new AgentRandomPatrolController(_character, _patrolRadius),
            _maxIdleTime); 

        _agentCharacterController.IsEnabled = true;
    }

    private void Update()
    {
        _agentCharacterController.IsEnabled = _character.IsAlive;

        _agentCharacterController.Update();
    }

}
