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
    private static Vector3[] s_BottomLocalVertices;
    
    [SerializeField] private TextMeshPro m_UnlockText;
    [FormerlySerializedAs ("m_TreePrefab")]
    [Title("Prefabs")]
    [SerializeField, AssetsOnly] private GameObject[] m_TreePrefabs;
    [SerializeField, AssetsOnly] private GameObject[] m_RockPrefabs;
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
            HexaZoneManager manager = HexaZoneManager.GetInstance();
            
            manager.GetCurrentContacting()?.m_UnlockText.gameObject.SetActive (false);
            manager.AddContacting (this);
            if (PlayDataManager.Instance.resourcePlayData.skull >= 5)
            {
                m_UnlockText.color= Color.white;
            }
            else
            {
                m_UnlockText.color = Color.red;
            }
            
            m_UnlockText.gameObject.SetActive (true);
        }
    }

    private void OnCollisionExit (Collision other)
    {
        if (other.gameObject.CompareTag ("Player"))
        {
            HexaZoneManager manager = HexaZoneManager.GetInstance();
            int index = manager.FindContactingIndex (this);
            if (index != -1)
            {
                manager.GetContacting (index).m_UnlockText.gameObject.SetActive (false);
                manager.RemoveContactingAt (index);
            }

            manager.GetCurrentContacting()?.m_UnlockText.gameObject.SetActive (true);
        }
    }

    public void Interact()
    {
        // Spawn objects //

        Vector3 randomPos = GetRandomPointInHexagon();

        bool tree_spawned = false;
        
        if (m_TreePrefabs.Length > 0) {
            if (Random.value < 0.5f) {
                int randNum = Random.Range (0, m_TreePrefabs.Length);
                Instantiate (m_TreePrefabs[randNum], randomPos, Quaternion.identity);
                tree_spawned = true;
            }
        }
        
        if (m_RockPrefabs.Length > 0) {
            if (!tree_spawned && Random.value < 0.5f) {
                int randNum = Random.Range (0, m_RockPrefabs.Length);
                Instantiate (m_RockPrefabs[randNum], randomPos, Quaternion.identity);
            }
        }
        
        
        // Destroy this Object //
        
        HexaZoneManager manager = HexaZoneManager.GetInstance();
        int index = manager.FindContactingIndex (this);
        if (index != -1) manager.RemoveContactingAt (index);
        manager.GetCurrentContacting()?.m_UnlockText.gameObject.SetActive (true);
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
