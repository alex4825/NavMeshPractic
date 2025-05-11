using UnityEngine;

public class PlayExample : MonoBehaviour
{
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _maxIdleTime;
    [SerializeField] private float _patrolRadius;

    private Controller _agentCharacterController;

    private float _idleTimer;

    private void Awake()
    {
        _agentCharacterController =
            new AgentRandomPatrolController(_character, _patrolRadius, _groundMask);
        //new AgentClickPointController(_character, _groundMask);

        _agentCharacterController.Enable();
    }

    private void Update()
    {
        _agentCharacterController.Update();
    }
}
