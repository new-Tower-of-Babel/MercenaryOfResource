using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static Coin instance;
    public int coin;
    public int resultWood;
    public int resultStone;
    public int resultSkull;
    public int resultGold;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResultResourceReset()
    {
        resultWood = 0;
        resultStone = 0;
        resultSkull = 0;
        resultGold = 0;
    }
    
}
