using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class RandomPatrolAgentMover : IMovable
{
    private NavMeshAgent _agent;
    private float _patrolRadius;

    public RandomPatrolAgentMover(NavMeshAgent agent, float moveSpeed, float patrolRadius)
    {
        _agent = agent;
        _agent.speed = moveSpeed;
        _patrolRadius = patrolRadius;

        _agent.updateRotation = false;
    }

    public Vector3 Velocity => _agent.desiredVelocity;

    public bool IsEnable { get; set; }

    public void UpdateMovement()
    {
        if (IsEnable)
            if (_agent.hasPath == false)
            {


                while (_agent.hasPath == false)
                {
                    _agent.destination = GetRandomDestinationInRadius();
                }

                //� ��������� ����������� ����� �����
                //����������� ��������� path � �����
                //���� path ������, �� �������
                //����� ������ ������� ����� � �������� �������� ���� � �.�.
            }
    }

    private Vector3 GetRandomDestinationInRadius()
    {
        float randomAngle = Random.Range(0, 360f);
        float randomDirectionLength = Random.Range(0, _patrolRadius);
        Vector3 moveDirectionNormalized = (Quaternion.Euler(0f, randomAngle, 0f) * _agent.transform.forward).normalized;

        return moveDirectionNormalized * randomDirectionLength;
    }
}
