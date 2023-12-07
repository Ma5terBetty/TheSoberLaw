using UnityEngine;

public interface IHealeable
{
    HealthController HealthController { get; }
    int MaxLife { get; }
    void GetHealing(float healAmount);
}
