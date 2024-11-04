using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{

    [Header("Special Stats")]
    [SerializeField] private float specialFireRate;
    [SerializeField] private float currentSpecialDamage;
    [SerializeField] private float currentSpecialDistance;
    [SerializeField] private float maxSpecialDamage;
    [SerializeField] private float maxSpecialDistance;
    [Header("Charge Stats")]
    [SerializeField] private float currentChargeTime;
    [SerializeField] private float maxChargeTime;
    [SerializeField] private float minChargeTime;

  
    protected override void SpecialShoot()
    {
        if (gunData.currentAmmo > 0 && CanShoot(specialFireRate))
        {
            RaycastHit[] hitsInfo = Physics.RaycastAll(cameraPos.position, cameraPos.forward, currentSpecialDistance);
            if (hitsInfo.Length > 0) {

                foreach (var hit in hitsInfo)
                {
                    IDamageable damageable = hit.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(currentSpecialDamage);
                }

                //just visual code will delete later
                Vector3 bulletDir = (hitsInfo[hitsInfo.Length - 1].point - muzzlePos.position).normalized;

                Transform bulletTransform = Instantiate(bulletPrefab, muzzlePos.position, Quaternion.identity);

                bulletTransform.GetComponent<Bullet>().Setup(bulletDir);
            }
                gunData.currentAmmo--;
                timeSinceLastShot = 0;
        }
    }

    private void GetChargeDistance(int currentChargeTime)
    {
        
    }
}
