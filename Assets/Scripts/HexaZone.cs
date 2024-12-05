using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class HexaZone : MonoBehaviour
{
    private static List<HexaZone> s_ContactingList = new();
    private static Vector3[] s_BottomLocalVertices;
    
    public static bool TryGetCurrentContacting(out HexaZone zone)
    {
        zone = null;
        
        if (s_ContactingList.Count == 0)
            return false;

        int lastIndex = s_ContactingList.Count - 1;
        zone = s_ContactingList[lastIndex];
        return true;
    }

    public static HexaZone GetCurrentContacting()
    {
        HexaZone zone;
        
        if (TryGetCurrentContacting (out zone))
            return zone;
        else
            return null;
    }

    public static void Clear()
    {
        s_ContactingList.Clear();
    }
  
    
    [SerializeField] private TextMeshPro m_UnlockText;
    [Title("Prefabs")]
    [SerializeField, AssetsOnly] private GameObject m_TreePrefab;
    [SerializeField, AssetsOnly] private GameObject m_RockPrefab;
    [SerializeField, AssetsOnly] private GameObject m_ChestPrefab;
    
    private MeshCollider m_MeshCollider;

    private Vector3 m_RandomDebugPoint;

    private void Awake()
    {
        m_MeshCollider = GetComponent<MeshCollider>();
    }

    private void Start()
    {
        StartCoroutine (DebugCoroutine());
    }

    IEnumerator DebugCoroutine()
    {
        while (true)
        {
            m_RandomDebugPoint = GetRandomPointInHexagon();
            yield return new WaitForSeconds (1f);
        }
    }

    private void OnCollisionEnter (Collision other)
    {
        if (other.gameObject.CompareTag ("Player"))
        {
            GetCurrentContacting()?.m_UnlockText.gameObject.SetActive (false);
            s_ContactingList.Add (this);
            m_UnlockText.gameObject.SetActive (true);
        }
    }

    private void OnCollisionExit (Collision other)
    {
        if (other.gameObject.CompareTag ("Player"))
        {
            int index = s_ContactingList.FindIndex (x => x == this);
            
            if (index != -1)
            {
                s_ContactingList[index].m_UnlockText.gameObject.SetActive (false);
                s_ContactingList.RemoveAt (index);
            }
            
            GetCurrentContacting()?.m_UnlockText.gameObject.SetActive (true);
        }
    }

    public void Interact()
    {
        // Spawn objects //

        Vector3 randomPos = GetRandomPointInHexagon();

        Instantiate (m_TreePrefab, randomPos, Quaternion.identity);
        
        
        // Destroy this Object //
        
        int index = s_ContactingList.FindIndex (x => x == this);
            
        if (index != -1)
            s_ContactingList.RemoveAt (index);
        
        GetCurrentContacting()?.m_UnlockText.gameObject.SetActive (true);
        
        Destroy (gameObject);
    }

    private Vector3[] GetBottomLocalVertices()
    {
        if (s_BottomLocalVertices != null)
            return s_BottomLocalVertices;
        
        Vector3[] colliderVertices = m_MeshCollider.sharedMesh.vertices;
        Vector3[] bottomVertices = new Vector3[6];
        int count = 0;
        
        foreach (Vector3 v3 in colliderVertices)
        {
            if (v3.y != 0f)
                continue;

            if (v3 is {x:0f, z:0f})
                continue;

            bool bExist = false;
            
            for (int i = 0; i < bottomVertices.Length; i++)
            {
                if (bottomVertices[i] == v3)
                {
                    bExist = true;
                    break;
                }
            }

            if (bExist)
                continue;

            bottomVertices[count++] = v3;
        }

        s_BottomLocalVertices = bottomVertices;
        return bottomVertices;
    }

    private Vector3[] GetBottomWorldVertices()
    {
        Vector3[] bottomLocalVertices = GetBottomLocalVertices();
        Vector3[] bottomWorldVertices = new Vector3[bottomLocalVertices.Length];

        for (int i = 0; i < bottomLocalVertices.Length; i++)
        {
            bottomWorldVertices[i] = bottomLocalVertices[i] + transform.position;
        }

        return bottomWorldVertices;
    }

    private Vector3 GetRandomPointInHexagon()
    {
        Vector3[] bottomVertices = GetBottomWorldVertices();
        
        int randomIndex = Random.Range (0, 6);

        Vector3 v0 = bottomVertices[randomIndex];
        Vector3 v1 = bottomVertices[(randomIndex + 1) % 6];
        Vector3 v2 = transform.position; // center

        float s = Random.value;
        float t = Random.value;

        if (s + t > 1)
        {
            s = 1 - s;
            t = 1 - t;
        }

        Vector3 randomPoint = s*v0 + t*v1 + (1-s-t)*v2;
        return randomPoint;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere (m_RandomDebugPoint, 0.5f);
    }
}
