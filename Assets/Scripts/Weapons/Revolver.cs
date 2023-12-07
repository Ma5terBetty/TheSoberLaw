using UnityEngine;

public class Revolver : BaseWeapon
{
    private int bulletIndex;

    private void Start()
    {
        shootingCooldown = 0;
        bulletBurst = 0;
        bulletIndex = 0;
    }

    private void Update()
    {
        if (shootingCooldown < Stats.FireCooldown && bulletIndex < Stats.BulletsPerShot)
        {
            shootingCooldown += Time.deltaTime;
        }
        else if (shootingCooldown >= Stats.FireCooldown && bulletIndex < Stats.BulletsPerShot && bulletBurst < Stats.FireRate)
        {
            bulletBurst += Time.deltaTime;
        }
    }

    public override void Shoot(bool isPlayerShooting)
    {
        if (IsShootAvailable())
        { 
            if (bulletBurst >= Stats.FireRate)
            {
                PrefabBullet bullet = Instantiate(Bullet, Noozle.position, transform.rotation);
                bullet.SetBullet(isPlayerShooting, Stats.Damage, Stats.AmmoSpeed);
                bulletBurst = 0;
                bulletIndex++;

                if (bulletIndex >= Stats.BulletsPerShot)
                {
                    shootingCooldown = 0;
                    bulletBurst = 0;
                    bulletIndex = 0;
                }
            }
        }
    }

    private bool IsShootAvailable()
    {
        return shootingCooldown >= Stats.FireCooldown;
    }
}
