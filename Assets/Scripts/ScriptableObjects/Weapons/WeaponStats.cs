using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName ="Stats/Weapon", order =1)]
public class WeaponStats : ScriptableObject
{
    [SerializeField] private WeaponStatsValues _stats;

    public float Damage => _stats.Damage;
    public float AmmoSpeed => _stats.AmmoSpeed;
    public float FireRate => _stats.FireRate;
    public int BulletsPerShot => _stats.BulletsPerShot;
}

[System.Serializable]
public struct WeaponStatsValues
{
    public float Damage;
    public float AmmoSpeed;
    public float FireRate;
    public int BulletsPerShot;
}
