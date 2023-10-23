using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour
{
    [SerializeField] private WeaponStats _stats;
    [SerializeField] private GameObject _bullet;

    public virtual void Shoot()
    { 
        
    }
}
