using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDieService
{
    private Dictionary<AgentCharacter, Func<bool>> _enemiesToDeadCondition;
    private List<AgentCharacter> _enemiesToDie;

    public EnemyDieService()
    {
        _enemiesToDeadCondition = new();
        _enemiesToDie = new();
    }

    public void Add(AgentCharacter enemy, Func<bool> deadCondition)
    {
        _enemiesToDeadCondition.Add(enemy, deadCondition);
    }

    public void Update()
    {
        foreach (KeyValuePair<AgentCharacter, Func<bool>> enemyConditionsPair in _enemiesToDeadCondition)
            if (enemyConditionsPair.Value())
                _enemiesToDie.Add(enemyConditionsPair.Key);

        if (_enemiesToDie.Count > 0)
        {
            foreach (AgentCharacter enemy in _enemiesToDie)
            {
                _enemiesToDeadCondition.Remove(enemy);
                enemy.Kill();
            }

            _enemiesToDie.Clear();
        }

        Debug.Log($"Enemy count: {_enemiesToDeadCondition.Count}");
    }
}
