using System;
using UnityEngine;

public class Revolver : WeaponBase
{
    private void Awake()
    {
        stats = ResourceManager.LoadSOData<WeaponStatsSO> ("Revolver");
    }

    public override void Fire (Vector3 position, Quaternion rotation)
    {
        var obj = ObjectPool.Instance.Spawn ("Projectile");
        obj.GetComponent<Projectile>().Setup (position, rotation);
        obj.SetActive (true);
    }
}
