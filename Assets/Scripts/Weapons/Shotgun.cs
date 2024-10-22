using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData gunData;

    [SerializeField] private Transform muzzlePos;
    [SerializeField] private Transform cameraPos;
    [SerializeField] private Transform bulletPrefab;

    [SerializeField] private float bulletPerShot;
    [SerializeField] private float inaccuracyDistance = 5f;

    private float timeSinceLastShot;

    // Start is called before the first frame update
    private void Start()
    {
        Player.ShootInput += Shoot;
        Player.ReloadInput += StartReload;
        gunData.isReloading = false;
    }

    // Update is called once per frame
    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(muzzlePos.position, muzzlePos.forward);
    }

    private void Shoot()
    {
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                for (int i = 0; i < bulletPerShot; i++)
                {
                    if (Physics.Raycast(cameraPos.position, GetShotingDirection(), out RaycastHit hitInfo, gunData.maxDistance))
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

    private Vector3 GetShotingDirection()
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