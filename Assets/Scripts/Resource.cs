using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resource : MonoBehaviour
{
    public enum ResourceType { Gold, Stone, Wood }

    public ResourceType type;
    public int maxHealth;
    public int curHealth;

    //public GameObject healthBar;
    public GameObject resourceRing;
    public float detectionRadius = 3f;

    void Start()
    {
        curHealth = maxHealth;

        //healthBar.GetComponent<Slider>().maxValue = maxHealth;
        //healthBar.GetComponent<Slider>().value = curHealth;

        //healthBar.SetActive(false);
        resourceRing.SetActive(false);
    }

    void Update()
    {
        //// 플레이어와 자원의 거리 계산
        //float distance = Vector3.Distance(자원 위치, 플레이어 위치);

        //// 플레이어가 범위 내에 있으면 원을 표시
        //if (distance <= detectionRadius)
        //{
        //    healthBar.SetActive(true);
        //    resourceRing.SetActive(true);
        //}
        //else
        //{
        //    healthBar.SetActive(false);
        //    resourceRing.SetActive(false);
        //}

        //// 체력을 표시하는 UI 업데이트
        //healthBar.GetComponent<Slider>().value = curHealth;
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
