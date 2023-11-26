using UnityEngine;

public class Shotgun : BaseWeapon
{
    [SerializeField] float _spreadAngle = 30;

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
            for (int i = 0; i < Stats.BulletsPerShot; i++)
            {
                PrefabBullet bullet = Instantiate(Bullet, Noozle.position, transform.rotation);
                bullet.SetBullet(isPlayerShooting, Stats.Damage, Stats.AmmoSpeed);
                bullet.transform.Rotate(new Vector3(0, 0, -_spreadAngle));
                bullet.transform.Rotate(new Vector3(0, 0, _spreadAngle * i));
            }
            shootingCooldown = 0;
        }
    }

    private bool IsShootAvailable()
    {
        return shootingCooldown >= Stats.FireCooldown;
    }
}
