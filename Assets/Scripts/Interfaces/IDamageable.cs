using UnityEngine;

public interface IDamageable
{
    HealthController HealthController { get; }
    int CurrentLife { get; }
    void GetDamage(float healAmount);
}
