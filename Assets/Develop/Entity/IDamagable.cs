public interface IDamagable
{
    float MaxHealth { get; }

    float Health { get; }

    bool IsHit { get; }

    void TakeDamage(float damage);
}
