using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private float bulletPerShot;
    [SerializeField] private float inaccuracyDistance = 5f;

    // Start is called before the first frame update


    protected override void Shoot()
    {
        Debug.Log("shotgun called");
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                for (int i = 0; i < bulletPerShot; i++)
                {
                    if (Physics.Raycast(cameraPos.position, GetShootingDirection(), out RaycastHit hitInfo, gunData.maxDistance))
                    {
                        //debug
                        Debug.Log(hitInfo.transform.name);
                        Debug.Log(hitInfo.transform.position);
                        Debug.DrawLine(cameraPos.position, hitInfo.point, Color.red, 5f);

                        //get bulletDir from hit point to muzzle of the gun
                        Vector3 bulletDir = (hitInfo.point - muzzlePos.position).normalized;

                        //init bulletprefab
                        Transform bulletTransform = Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);

                        //move bullet prefab
                        bulletTransform.GetComponent<Bullet>().Setup(bulletDir);

                        //hand logic in data
                        IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                        damageable?.TakeDamage(gunData.damage);
                    }
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
            }
        }
    }

    protected override void SpecialShoot()
    {
        throw new System.NotImplementedException();
    }

    private Vector3 GetShootingDirection()
    {
        Vector3 targetPos = cameraPos.position + cameraPos.forward * gunData.maxDistance;
        targetPos = new Vector3(
              targetPos.x + Random.Range(-inaccuracyDistance, inaccuracyDistance),
              targetPos.y + Random.Range(-inaccuracyDistance, inaccuracyDistance),
              targetPos.z + Random.Range(-inaccuracyDistance, inaccuracyDistance));

        Vector3 direction = targetPos - cameraPos.position;
        return direction.normalized;
    }

    private void OnDisable()
    {
        Player.ShootInput -= this.Shoot;
        Player.ReloadInput -= base.StartReload;
        gunData.isReloading = false;
    }

    private void OnEnable()
    {
        Player.ShootInput += this.Shoot;
        Player.ReloadInput += base.StartReload;
        gunData.isReloading = false;
    }
}