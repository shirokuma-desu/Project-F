using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    [SerializeField] private float bulletPerShot;
    [SerializeField] private float inaccuracyDistance = 5f;

    protected override void SpecialShoot()
    {
        if (gunData.currentAmmo > 0 && CanShoot(gunData.fireRate))
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
  
}