using System;
using UnityEngine;

public class Weapon : WeaponBase
{
    public Weapon()
    {
        stats = SelectList.instance.WeaponDataDic[SelectList.instance.selectedWeapon];
    }

    public override void Fire (Vector3 position, Quaternion rotation)
    {
        AudioManager.Instance.PlayFireSFX();
        var obj = ObjectPools.Instance.Spawn ("Projectile");
        obj.GetComponent<Projectile>().Setup (stats, position, rotation);
    }
}
