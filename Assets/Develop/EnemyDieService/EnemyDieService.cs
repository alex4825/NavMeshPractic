using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieService : MonoBehaviour
{
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private float _minTimeToDie;
    [SerializeField] private float _maxTimeToDie;
    [SerializeField] private int _maxEnemies;

    private Dictionary<AgentCharacter, Func<bool>[]> _enemiesDeadConditions;
    private List<AgentCharacter> _enemiesToDie;

    private float TimeToDie => UnityEngine.Random.Range(_minTimeToDie, _maxTimeToDie);

    private void Awake()
    {
        _enemyFactory.OnSpawn += OnEnemySpawn;
        _enemiesDeadConditions = new();
        _enemiesToDie = new();
    }

    private void Update()
    {
        foreach (KeyValuePair<AgentCharacter, Func<bool>[]> enemyConditionsPair in _enemiesDeadConditions)
            foreach (Func<bool> condition in enemyConditionsPair.Value)
                if (condition())
                    _enemiesToDie.Add(enemyConditionsPair.Key);

        if (_enemiesToDie.Count > 0)
        {
            foreach (AgentCharacter enemy in _enemiesToDie)
            {
                _enemiesDeadConditions.Remove(enemy);
                enemy.Kill();
            }

            _enemiesToDie.Clear();
        }

        Debug.Log($"Enemy count: {_enemiesDeadConditions.Count}");
    }

    private void OnDestroy()
    {
        _enemyFactory.OnSpawn -= OnEnemySpawn;
    }

    private void OnEnemySpawn(AgentCharacter enemy, bool isDeadActive, bool isTimeLeftActive, bool isTooMuchEnemiesActive)
    {
        List<Func<bool>> deadConditions = new List<Func<bool>>();

        if (isDeadActive)
            deadConditions.Add(() => enemy.IsDead);

        if (isTimeLeftActive)
            deadConditions.Add(() => enemy.Lifetime >= TimeToDie);

        if (isTooMuchEnemiesActive)
            deadConditions.Add(() => _enemiesDeadConditions.Count > _maxEnemies);


        _enemiesDeadConditions.Add(enemy, deadConditions.ToArray());
    }

}
