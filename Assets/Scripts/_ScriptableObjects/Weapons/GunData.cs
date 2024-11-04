using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="GunDataSO", menuName ="Weapon/Gun")]
public class GunData : ScriptableObject
{
    [Header("Information")]
    public string gunName;

    [Header("Shooting stats")]
    public float damage;
    public float maxDistance;
    public float fireRate;


    [Header("Reloading Stats")]
    public int currentAmmo;
    public int magSize;
    public float reloadTime;

    public bool isReloading;
    
}
