using UnityEngine;

public interface IDamagable : ITransformPosition
{
    float MaxHealth { get; }

    float Health { get; }

    bool IsHit { get; }

    void TakeDamage(float damage);
}
