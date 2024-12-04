using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class HexaZone : MonoBehaviour
{
    private static List<HexaZone> s_InteractingList = new();

    public static bool TryGetCurrent(out HexaZone zone)
    {
        zone = null;
        
        if (s_InteractingList.Count == 0)
            return false;

        int lastIndex = s_InteractingList.Count - 1;
        zone = s_InteractingList[lastIndex];
        return true;
    }

    public static HexaZone GetCurrent()
    {
        HexaZone zone;
        
        if (TryGetCurrent (out zone))
            return zone;
        else
            return null;
    }

    public static void Clear()
    {
        s_InteractingList.Clear();
    }
  
    
    [SerializeField] private TextMeshPro m_UnlockText;

    private void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.CompareTag ("Player"))
        {
            GetCurrent()?.m_UnlockText.gameObject.SetActive (false);
            s_InteractingList.Add (this);
            m_UnlockText.gameObject.SetActive (true);
        }
    }

    private void OnCollisionExit (Collision other)
    {
        if (other.gameObject.CompareTag ("Player"))
        {
            int index = s_InteractingList.FindIndex (x => x == this);
            
            if (index != -1)
            {
                s_InteractingList[index].m_UnlockText.gameObject.SetActive (false);
                s_InteractingList.RemoveAt (index);
            }
            
            GetCurrent()?.m_UnlockText.gameObject.SetActive (true);
        }
    }

    public void Interact()
    {
        int index = s_InteractingList.FindIndex (x => x == this);
            
        if (index != -1)
            s_InteractingList.RemoveAt (index);
        
        GetCurrent()?.m_UnlockText.gameObject.SetActive (true);
        
        Destroy (gameObject);
    }
}
