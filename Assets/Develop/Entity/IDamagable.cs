public interface IDamagable : ITransformPosition
{
    bool IsHit { get; }

    void TakeDamage(float damage);
}
