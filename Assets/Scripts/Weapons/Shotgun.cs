using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : BaseWeapon
{
    private void Start()
    {
        shootingCooldown = 0;
    }

    private void Update()
    {
        if (!IsShootAvailable())
        {
            shootingCooldown += Time.deltaTime;
        }
    }

    public override void Shoot()
    {
        if (IsShootAvailable())
        {
            PrefabBullet enemyBullet = Instantiate(Bullet, Noozle.position, transform.rotation);
        }
    }

    private bool IsShootAvailable()
    {
        return shootingCooldown >= Stats.FireCooldwon;
    }
}
