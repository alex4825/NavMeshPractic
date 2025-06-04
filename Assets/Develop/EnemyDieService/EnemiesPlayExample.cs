using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesPlayExample : MonoBehaviour
{
    [SerializeField] private AgentCharacter _enemyPrefab;
    [SerializeField] private Transform _enemySpawnPoint;
    [SerializeField] private float _patrolRadius;

    [SerializeField] private DeadKeysListener _deadKeysListener;

    [SerializeField] private float _timeToDie;
    [SerializeField] private int _maxEnemies;

    private EnemyFactory _enemyFactory;
    private EnemyDieService _enemyDieService;

    private Dictionary<AgentCharacter, Controller> _enemiesToController;


    private void Awake()
    {
        _enemyDieService = new();

        _enemyFactory = new(_enemyPrefab, _enemySpawnPoint);

        _deadKeysListener.DeadTypesSelected += OnDeadTypesSelected;

        _enemiesToController = new();
    }

    private void Update()
    {
        foreach (var enemyController in _enemiesToController)
        {
            enemyController.Value.IsEnabled = enemyController.Key.IsAlive;
            enemyController.Value.Update();
        }

        _enemyDieService.Update();
    }

    private void OnDestroy()
    {
        _deadKeysListener.DeadTypesSelected -= OnDeadTypesSelected;
    }

    private void OnDeadTypesSelected(DeadTypes[] deadTypes)
    {
        AgentCharacter enemy = _enemyFactory.SpawnEnemy(); 

        GenerateControllerFor(enemy);

        _enemyDieService.Add(enemy, GetCompositeCondition(enemy, deadTypes));
    }

    private Func<bool> GetCompositeCondition(AgentCharacter enemy, DeadTypes[] deadTypes)
    {
        List<Func<bool>> compositeCondition = new();

        foreach (DeadTypes deadType in deadTypes)
            switch (deadType)
            {
                case DeadTypes.IsDead:
                    compositeCondition.Add(() => enemy.IsDead);
                    break;

                case DeadTypes.TimeLeft:
                    compositeCondition.Add(() => enemy.Lifetime >= _timeToDie);
                    break;

                case DeadTypes.TooMuchEntities:
                    compositeCondition.Add(() => _enemiesToController.Count > _maxEnemies);
                    break;
            }

        return () =>
        {
            foreach (var condition in compositeCondition)
                if (condition())
                    return true;

            return false;
        };
    }

    private void GenerateControllerFor(AgentCharacter enemy)
    {
        Controller enemyController = new AgentRandomPatrolController(enemy, _patrolRadius);
        _enemiesToController.Add(enemy, enemyController);
        enemyController.IsEnabled = true;

        enemy.Dead += OnEnemyDead;
    }

    private void OnEnemyDead(AgentCharacter enemy, float deadDuration)
    {
        _enemiesToController[enemy].IsEnabled = false;
        _enemiesToController.Remove(enemy);

        enemy.Dead -= OnEnemyDead;
    }
}
