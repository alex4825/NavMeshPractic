using UnityEngine;

public interface IDamagable
{
    float MaxHealth { get; }

    float Health { get; }

    bool IsHit { get; }

    Vector3 Position { get; }

    void TakeDamage(float damage);
}
