using UnityEngine;

public interface IShooteable
{
    BaseWeapon BaseWeapon { get; }
    void Shoot();
}
