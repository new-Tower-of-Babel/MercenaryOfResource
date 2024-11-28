using UnityEngine;

public abstract class WeaponBase
{
    protected WeaponStatsSO stats;

    public abstract void Fire (Vector3 position, Quaternion rotation);
}
