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

    public override void Shoot()
    {
        if (IsShootAvailable())
        {
            PrefabBullet bullet = Instantiate(Bullet, Noozle.position, transform.rotation);
            //bullet.SetBullet();
        }
    }

    private bool IsShootAvailable()
    { 
        return shootingCooldown >= Stats.FireCooldwon;
    }
}
