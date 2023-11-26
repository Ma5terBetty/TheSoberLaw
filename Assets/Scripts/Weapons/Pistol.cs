using UnityEngine;

public class Pistol : BaseWeapon
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

    public override void Shoot(bool isPlayerShooting)
    {
        if (IsShootAvailable())
        {
            PrefabBullet bullet = Instantiate(Bullet, Noozle.position, transform.rotation);
            bullet.SetBullet(isPlayerShooting, Stats.Damage, Stats.AmmoSpeed);
            shootingCooldown = 0;
        }
    }

    public bool IsShootAvailable()
    { 
        return shootingCooldown >= Stats.FireCooldown;
    }
}
