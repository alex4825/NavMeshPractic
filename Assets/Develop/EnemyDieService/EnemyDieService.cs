using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieService : MonoBehaviour
{
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private float _minTimeToDie;
    [SerializeField] private float _maxTimeToDie;
    [SerializeField] private int _maxEnemies;
    
    private Dictionary<AgentCharacter, Func<bool>[]> _enemiesToDeadConditions;
    private List<AgentCharacter> _enemiesToDie;

    private float TimeToDie => UnityEngine.Random.Range(_minTimeToDie, _maxTimeToDie);

    private void Awake()
    {
        _enemyFactory.OnSpawn += OnEnemySpawn;
        _enemiesToDeadConditions = new();
        _enemiesToDie = new();
    }

    private void Update()
    {
        foreach (KeyValuePair<AgentCharacter, Func<bool>[]> enemyConditionsPair in _enemiesToDeadConditions)
            foreach (Func<bool> condition in enemyConditionsPair.Value)
                if (condition())
                    _enemiesToDie.Add(enemyConditionsPair.Key);

        if (_enemiesToDie.Count > 0)
        {
            foreach (AgentCharacter enemy in _enemiesToDie)
            {
                _enemiesToDeadConditions.Remove(enemy);
                enemy.Kill();
            }

            _enemiesToDie.Clear();
        }

        Debug.Log($"Enemy count: {_enemiesToDeadConditions.Count}");
    }

    private void OnDestroy()
    {
        _enemyFactory.OnSpawn -= OnEnemySpawn;
    }

    private void OnEnemySpawn(AgentCharacter enemy, DeadTypes[] deadTypes)
    {
        List<Func<bool>> deadConditions = new List<Func<bool>>();

        foreach (DeadTypes deadType in deadTypes)
            switch (deadType)
            {
                case DeadTypes.IsDead:
                    deadConditions.Add(() => enemy.IsDead);
                    break;

                case DeadTypes.TimeLeft:
                    deadConditions.Add(() => enemy.Lifetime >= TimeToDie);
                    break;

                case DeadTypes.TooMuchEntities:
                    deadConditions.Add(() => _enemiesToDeadConditions.Count > _maxEnemies);
                    break;
            }

        _enemiesToDeadConditions.Add(enemy, deadConditions.ToArray());
    }
}
