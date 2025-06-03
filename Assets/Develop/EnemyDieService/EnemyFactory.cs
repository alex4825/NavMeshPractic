using System;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private AgentCharacter _enemyPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private DeadKeysListener _keysListener;

    public event Action<AgentCharacter, DeadTypes[]> OnSpawn;

    private void Awake()
    {
        _keysListener.KeysSelected += SpawnEnemy;
    }

    private void OnDestroy()
    {
        _keysListener.KeysSelected -= SpawnEnemy;
    }

    private void SpawnEnemy(DeadTypes[] deadTypes)
    {
        AgentCharacter enemy = Instantiate(_enemyPrefab, _spawnPoint);

        OnSpawn?.Invoke(enemy, deadTypes);
    }
}
