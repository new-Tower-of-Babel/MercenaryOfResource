using System;
using UnityEngine;
using UnityEngine.Events;

public class MonsterBase : MonoBehaviour
{
    protected MonsterStatsSO stats;

    public UnityEvent<GameObject> OnDied;

    private int testHealth = 10;

    public void Die()
    {
        gameObject.SetActive (false);
        ObjectPool.Instance.Despawn (gameObject);
        OnDied?.Invoke(gameObject);
    }

    public void TakeDamage (int amount)
    {
        testHealth -= amount;
        if (testHealth <= 0)
        {
            Die();            
        }
    }
}
