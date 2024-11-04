using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected GunData gunData;
    [SerializeField] protected Transform muzzlePos;
    [SerializeField] protected Transform cameraPos;
    [SerializeField] protected Transform bulletPrefab;


    protected float timeSinceLastShot;

    // Start is called before the first frame update
  
    // Update is called once per frame
    void Update()
    {
        timeSinceLastShot += Time.deltaTime;

        Debug.DrawRay(muzzlePos.position, muzzlePos.forward);
    }


    protected virtual void Shoot()
    {
        if (gunData.currentAmmo > 0 && CanShoot(gunData.fireRate))
        {
                if (Physics.Raycast(cameraPos.position, cameraPos.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    Debug.Log(hitInfo.transform.name);
                    Debug.Log(hitInfo.transform.position);
                    Debug.DrawLine(cameraPos.position, hitInfo.point, Color.red, 5f);

                    Vector3 bulletDir = (hitInfo.point - muzzlePos.position).normalized;

                    Transform bulletTransform = Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);

                    bulletTransform.GetComponent<Bullet>().Setup(bulletDir);

                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
        }
    }
    protected abstract void SpecialShoot();

    protected void StartReload()
    {
        if (!gunData.isReloading && this.gameObject.activeSelf)
        {
            StartCoroutine(Reload());
        }
    }

    protected IEnumerator Reload()
    {
        gunData.isReloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;
        gunData.isReloading = false;
    }

    protected virtual bool CanShoot(float fireRate) => !gunData.isReloading && timeSinceLastShot > 1f / (fireRate / 60f);

    protected virtual void OnEnable()
    {
        Player.ShootEvent += Shoot;
        Player.ReloadEvent += StartReload;
        Player.SpecialShootEvent += SpecialShoot;
        gunData.isReloading = false;
    }

    protected virtual void OnDisable()
    {
        Player.ShootEvent -= Shoot;
        Player.ReloadEvent -= StartReload;
        Player.SpecialShootEvent -= SpecialShoot;
        gunData.isReloading = false;
    }
}
