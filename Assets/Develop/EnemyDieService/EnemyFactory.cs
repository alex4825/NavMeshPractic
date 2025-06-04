using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory
{
    private AgentCharacter _enemyPrefab;
    private Transform _spawnPoint;

    private List<AgentCharacter> _enemies;

    public EnemyFactory(AgentCharacter enemyPrefab, Transform spawnPoint)
    {
        _enemyPrefab = enemyPrefab;
        _spawnPoint = spawnPoint;

        _enemies = new();
    }

    public AgentCharacter SpawnEnemy()
    {
        AgentCharacter enemy = UnityEngine.Object.Instantiate(_enemyPrefab, _spawnPoint);

        _enemies.Add(enemy);

        enemy.Dead += OnEnemyDead;

        return enemy;
    }

    private void OnEnemyDead(AgentCharacter enemy, float deadTime)
    {
        _enemies.Remove(enemy);
    }
}
