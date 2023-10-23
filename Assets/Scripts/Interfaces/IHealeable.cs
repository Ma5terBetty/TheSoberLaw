using UnityEngine;

public interface IHealeable
{
    HealthController HealthController { get; }
    int MaxLife { get; }
    int CurrentLife { get; }
    void GetHealing(float healAmount);
}
