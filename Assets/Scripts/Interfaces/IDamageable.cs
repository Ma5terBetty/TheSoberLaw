using UnityEngine;

public interface IDamageable
{
    HealthController HealthController { get; }
    void GetDamage(float damageAmount);
}
