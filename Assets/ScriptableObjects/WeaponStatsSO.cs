using UnityEngine;

[CreateAssetMenu(menuName = "SOData/WeaponStats")]
public class WeaponStatsSO : ScriptableObject
{
    public string WeaponName;
    public float damage;
    public int projectilesPerShot;
    public float fireRate;
    public float reloadTime;
    public int maxAmmo;
    public float criticalDamageChance;
    public float bulletBounceChance;
    public int needCoin;
}
