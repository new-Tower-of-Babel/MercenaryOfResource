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
        // 플레이어가 콜라이더 상태에 있으면 나무 주위에 원을 표시

        // 체력을 표시하는 UI 업데이트
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
        // 파편화된 오브젝트 생성
        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        GameObject fragment = Instantiate(fragmentPrefab, spawnPosition, Quaternion.identity);
        fragment.GetComponent<ResourceFragment>().type = this.type;
    }
}
