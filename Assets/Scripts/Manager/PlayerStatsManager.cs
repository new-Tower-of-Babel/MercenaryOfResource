using System;
using UnityEngine;

public static class PlayerStatsManager
{
    private static GunslingerStatsSO m_GunslingerStats;
    private static WeaponStatsSO m_WeaponStats;

    public static WeaponStatsSO WeaponStats
    {
        get {
            if (m_WeaponStats == null)
                m_WeaponStats = ResourceManager.LoadSOData<WeaponStatsSO> ("Revolver");
            return m_WeaponStats;
        }
    }

    public static GunslingerStatsSO GunslingerStats
    {
        get {
            if (m_GunslingerStats == null)
                m_GunslingerStats = ResourceManager.LoadSOData<GunslingerStatsSO> ("OldMan");
            return m_GunslingerStats;
        }
    }
}
