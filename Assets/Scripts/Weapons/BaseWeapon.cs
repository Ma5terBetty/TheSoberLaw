using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] private WeaponStats _weaponStats;
    [SerializeField] private PrefabBullet _bullet;
    [SerializeField] private Transform _noozle;

    protected PrefabBullet Bullet => _bullet;
    protected WeaponStats Stats => _weaponStats;
    protected Transform Noozle => _noozle;
    protected float shootingCooldown;
    protected float bulletBurst;

    public virtual void Shoot(bool isPlayerShooting)
    { 
        
    }
}
