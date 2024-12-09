using System;
using UnityEngine;
using System.Collections.Generic;

public class HexaZoneManager : MonoBehaviour
{
    private static HexaZoneManager s_Instance;
    public static HexaZoneManager GetInstance()
    {
        return s_Instance;
    }


    private List<HexaZone> m_ContactingList = new();

    
    
    private void Awake()
    {
        s_Instance = this;
    }
    
    
    
    
    public void AddContacting (HexaZone hexaZone)
    {
        m_ContactingList.Add (hexaZone);
    }

    public int FindContactingIndex (HexaZone hexaZone)
    {
        return m_ContactingList.FindIndex(x => x == hexaZone);
    }

    public void RemoveContactingAt (int index)
    {
        m_ContactingList.RemoveAt (index);
    }

    public HexaZone GetContacting (int index)
    {
        return m_ContactingList[index];
    }

    public HexaZone GetCurrentContacting()
    {
        if (m_ContactingList.Count == 0)
        {
            return null;
        }

        int lastIndex = m_ContactingList.Count - 1;
        return m_ContactingList[lastIndex];
    }
}
