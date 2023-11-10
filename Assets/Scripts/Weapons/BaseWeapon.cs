using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] private WeaponStats _stats;
    [SerializeField] private PrefabBullet _bullet;
    [SerializeField] private Transform _noozle;

    protected PrefabBullet Bullet => _bullet;
    protected WeaponStats Stats => _stats;
    protected Transform Noozle => _noozle;
    protected float shootingCooldown;

    public virtual void Shoot()
    { 
        
    }
}
