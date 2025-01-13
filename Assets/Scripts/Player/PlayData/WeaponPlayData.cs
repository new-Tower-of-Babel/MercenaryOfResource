using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponPlayData : MonoBehaviour
{
    public float damage;
    public int projectilesPerShot;
    public float fireRate;
    public float reloadTime;
    public int maxAmmo;
    public float criticalDamageChance;
    public float bulletBounceChance;
    
    private void Start()
    {
        SelectList.instance.SelectWeapon();
        startWeaponStatsSeting();
    }


    private void startWeaponStatsSeting()
    {
        string key = SelectList.instance.WeaponDic.FirstOrDefault(x => x.Value == true).Key;
        WeaponStatsSO selectingWeapon = SelectList.instance.weaponSoList.Find(item => item.WeaponName == key);
        damage = selectingWeapon.damage;
        projectilesPerShot = selectingWeapon.projectilesPerShot;
        fireRate = selectingWeapon.fireRate;
        reloadTime = selectingWeapon.reloadTime;
        maxAmmo = selectingWeapon.maxAmmo;
        criticalDamageChance = selectingWeapon.criticalDamageChance;
        bulletBounceChance = selectingWeapon.bulletBounceChance;
    }
}
