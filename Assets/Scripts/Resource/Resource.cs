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
    //public GameObject resourceRing;

    public GameObject fragmentPrefab;

    void Start()
    {
        curHealth = maxHealth;

        //healthBar.GetComponent<Slider>().maxValue = maxHealth;
        //healthBar.GetComponent<Slider>().value = curHealth;

        //healthBar.SetActive(false);
        //resourceRing.SetActive(false);
    }

    void Update()
    {
        // �÷��̾ �ݶ��̴� ���¿� ������ ���� ������ ���� ǥ��

        // ü���� ǥ���ϴ� UI ������Ʈ
        //healthBar.GetComponent<Slider>().value = curHealth;
    }

    public void TakeDamage(int damage)
    {
        this.curHealth -= damage;

        if(this.curHealth <= 0)
        {
            Destroy(this.gameObject);
            FragmentResource();
        }
    }

    private void FragmentResource()
    {
        // ����ȭ�� ������Ʈ ����
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        GameObject fragment = Instantiate(fragmentPrefab, spawnPosition, Quaternion.identity);
        fragment.GetComponent<ResourceFragment>().type = this.type;
    }
}
