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
        //// �÷��̾�� �ڿ��� �Ÿ� ���
        //float distance = Vector3.Distance(�ڿ� ��ġ, �÷��̾� ��ġ);

        //// �÷��̾ ���� ���� ������ ���� ǥ��
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

        //// ü���� ǥ���ϴ� UI ������Ʈ
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
