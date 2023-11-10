using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName ="Stats/Weapon", order =1)]
public class WeaponStats : ScriptableObject
{
    [SerializeField] private WeaponStatsValues _stats;

    public float Damage => _stats.Damage;
    public float AmmoSpeed => _stats.AmmoSpeed;
    public float FireRate => _stats.FireRate;
    public float FireCooldwon => _stats.FireCooldown;
    public int BulletsPerShot => _stats.BulletsPerShot;
}

[System.Serializable]
public struct WeaponStatsValues
{
    public float Damage; // Daño del arma
    public float AmmoSpeed; // Velocidad de munición
    public float FireRate; // Frecuencia de disparos
    public float FireCooldown; // Tiempo entre disparos
    public int BulletsPerShot; // Balas emitidas por disparo
}
