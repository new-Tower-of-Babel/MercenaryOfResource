using System;
using UnityEngine;

public class Revolver : WeaponBase
{
    public Revolver()
    {
        stats = ResourceManager.LoadSOData<WeaponStatsSO>("Revolver");
    }

    public override void Fire (Vector3 position, Quaternion rotation)
    {
        AudioManager.Instance.PlayFireSFX();
        var obj = ObjectPools.Instance.Spawn ("Projectile");
        obj.GetComponent<Projectile>().Setup (stats, position, rotation);
    }
}
