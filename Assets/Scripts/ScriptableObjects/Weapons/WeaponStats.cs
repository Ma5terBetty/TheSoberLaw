using UnityEditor;
using UnityEditor.TerrainTools;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName ="Stats/Weapon", order =1)]
public class WeaponStats : ScriptableObject
{
    [SerializeField] private WeaponStatsValues _stats;

    public float Damage => _stats.Damage;
    public float AmmoSpeed => _stats.AmmoSpeed;
    public float FireRate => _stats.FireRate;
    public float FireCooldown => _stats.FireCooldown;
    public int BulletsPerShot => _stats.BulletsPerShot;
    public Sprite Sprite => _stats.Weapon;

}

[System.Serializable]
public struct WeaponStatsValues
{
    public float Damage; // Daño del arma
    public float AmmoSpeed; // Velocidad de munición
    public float FireCooldown; // Frecuencia de disparos
    public float FireRate; // Tiempo entre disparos
    public int BulletsPerShot; // Balas emitidas por disparo
    public Sprite Weapon;
}
