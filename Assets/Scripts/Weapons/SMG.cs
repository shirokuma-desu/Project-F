using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMG : Weapon
{
    protected override void Shoot()
    {
        base.CoreShoot();
    }

    protected override void SpecialShoot()
    {
        throw new NotImplementedException();
    }

    private void OnDisable()
    {
        Player.ShootInput -= this.Shoot;
        Player.ReloadInput -= StartReload;
        gunData.isReloading = false;
    }
    private void OnEnable()
    {
        Player.ShootInput += this.Shoot;
        Player.ReloadInput += StartReload;
        gunData.isReloading = false;
    }
}
