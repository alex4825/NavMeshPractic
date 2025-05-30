using System.Collections.Generic;
using UnityEngine;

public class PlayExample : MonoBehaviour
{
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private AgentCharacter _character;
    [SerializeField] private LayerMask _groundMask;

    [SerializeField] private float _maxIdleTime;
    [SerializeField] private float _patrolRadius;

    private Controller _agentCharacterController;

    private Dictionary<AgentCharacter, Controller> _enemiesControllers;

    private void Awake()
    {
        _agentCharacterController = new CompositeController(
            new AgentClickPointController(_character, _groundMask),
            new AgentRandomPatrolController(_character, _patrolRadius),
            _maxIdleTime);

        _agentCharacterController.IsEnabled = true;

        _enemiesControllers = new();
        _enemyFactory.OnSpawn += OnEnemyFactorySpawn;
    }

    private void Update()
    {
        _agentCharacterController.IsEnabled = _character.IsAlive;

        _agentCharacterController.Update();

        foreach (var enemyController in _enemiesControllers)
        {
            enemyController.Value.IsEnabled = enemyController.Key.IsAlive;
            enemyController.Value.Update();
        }
    }

    private void OnDestroy()
    {
        _enemyFactory.OnSpawn -= OnEnemyFactorySpawn;
    }

    private void OnEnemyFactorySpawn(AgentCharacter enemy, bool arg2, bool arg3, bool arg4)
    {
        Controller enemyController = new AgentRandomPatrolController(enemy, _patrolRadius);
        _enemiesControllers.Add(enemy, enemyController);
        enemyController.IsEnabled = true;

        enemy.Dead += (float deadDuration) =>
        {
            _enemiesControllers.Remove(enemy);
        };
    }

}
