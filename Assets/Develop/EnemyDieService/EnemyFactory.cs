using System;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private AgentCharacter _enemyPrefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private KeysListener _keysListener;

    public event Action<AgentCharacter, bool, bool, bool> OnSpawn;

    private void Awake()
    {
        _keysListener.KeysSelected += SpawnEnemy;
    }

    private void OnDestroy()
    {
        _keysListener.KeysSelected -= SpawnEnemy;
    }

    private void SpawnEnemy(bool isDeadActive, bool isTimeLeftActive, bool isTooMuchEnemiesActive)
    {
        AgentCharacter enemy = Instantiate(_enemyPrefab, _spawnPoint);

        OnSpawn?.Invoke(enemy, isDeadActive, isTimeLeftActive, isTooMuchEnemiesActive);
    }
}
