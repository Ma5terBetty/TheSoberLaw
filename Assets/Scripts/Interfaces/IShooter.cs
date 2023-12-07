using UnityEngine;

public interface IShooter
{
    BaseWeapon BaseWeapon { get; }
    void Shoot();
    bool IsPlayer();
}
