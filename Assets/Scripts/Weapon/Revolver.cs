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
        var obj = ObjectPool.Instance.Spawn ("Projectile");
        obj.GetComponent<Projectile>().Setup (stats, position, rotation);
    }
}
