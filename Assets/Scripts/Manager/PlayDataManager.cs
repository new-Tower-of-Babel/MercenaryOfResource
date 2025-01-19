using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDataManager : MonoBehaviour
{
    public static PlayDataManager Instance;
    public CharacterPlayData characterPlayData;
    public ResourcePlayData resourcePlayData;
    public WeaponPlayData weaponPlayData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        characterPlayData = GetComponent<CharacterPlayData>();
        resourcePlayData = GetComponent<ResourcePlayData>();
        weaponPlayData = GetComponent<WeaponPlayData>();
    }
    
}
