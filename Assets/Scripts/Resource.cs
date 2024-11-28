using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public enum ResourceType { Gold, Stone, Wood }

    public ResourceType type;
    public int maxHealth;
    public int curHealth;

    void Start()
    {
        curHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        this.curHealth -= damage;

        if(this.curHealth < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
