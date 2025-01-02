using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.WSA;

public class PlayData : MonoBehaviour
{
    
    public int maxHealth;
    public float moveSpeed;
    public int axeDamage;
    public int pickaxeDamage;
    public float rockHarvestingSpeed;
    public float woodHarvestingSpeed;
    public int resourceCarryLimit;
    
    public float damage;
    public int projectilesPerShot;
    public float fireRate;
    public float reloadTime;
    public int maxAmmo;
    public float criticalDamageChance;
    public float bulletBounceChance;

    public int gold = 0;
    public int wood = 0;
    public int stone = 0;
    public int skull = 0;

    private void Start()
    {
        startCharacterStatsSeting();
        startWeaponStatsSeting();
    }

    private void startCharacterStatsSeting()
    {
        string _key = SelectList.Instance.characterDic.FirstOrDefault(x => x.Value == true).Key;
        GunslingerStatsSO selectingCharacter =
            SelectList.Instance.characterSoList.Find(item => item.CharacterName == _key);
        maxHealth = selectingCharacter.maxHealth;
        moveSpeed = selectingCharacter.moveSpeed;
        axeDamage = selectingCharacter.axeDamage;
        pickaxeDamage = selectingCharacter.pickaxeDamage;
        rockHarvestingSpeed = selectingCharacter.rockHarvestingSpeed;
        woodHarvestingSpeed = selectingCharacter.woodHarvestingSpeed;
        resourceCarryLimit = selectingCharacter.resourceCarryLimit;
    }

    private void startWeaponStatsSeting()
    {
        string _key = SelectList.Instance.WeaponDic.FirstOrDefault(x => x.Value == true).Key;
        WeaponStatsSO selectingWeapon = SelectList.Instance.weaponSoList.Find(item => item.WeaponName == _key);
        damage = selectingWeapon.damage;
        projectilesPerShot = selectingWeapon.projectilesPerShot;
        fireRate = selectingWeapon.fireRate;
        reloadTime = selectingWeapon.reloadTime;
        maxAmmo = selectingWeapon.maxAmmo;
        criticalDamageChance = selectingWeapon.criticalDamageChance;
        bulletBounceChance = selectingWeapon.bulletBounceChance;
    }
    
}
