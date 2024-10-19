using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform muzzlePos;
    [SerializeField] private Transform bulletPrefab;
    

    private float timeSinceLastShot;

    // Start is called before the first frame update
    void Start()
    {
        Player.ShootInput += Shoot;
        Player.ReloadInput += StartReload;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(muzzlePos.position, muzzlePos.forward);
    }

    private void Shoot()
    {
        if(gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                if(Physics.Raycast(muzzlePos.position, muzzlePos.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    Debug.Log(hitInfo.transform.name);
                    Debug.Log(hitInfo.transform.position);

                    Vector3 bulletDir = (hitInfo.point - muzzlePos.position).normalized;

                    Transform bulletTransform = Instantiate(bulletPrefab, muzzlePos.position,Quaternion.identity);

                    bulletTransform.GetComponent<Bullet>().Setup(bulletDir);

                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
    }

    private void OnGunShot()
    {
       
    }

    private void StartReload()
    {
        if (!gunData.isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        gunData.isReloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;
        gunData.isReloading = false;
    }

    private bool CanShoot() => !gunData.isReloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);

    private void OnDisable()
    {
        Player.ShootInput -= Shoot;
        Player.ReloadInput -= StartReload;
    }
}
