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
    private bool isvisible = false;

    public GameObject fragmentPrefab;

    void Awake()
    {
        
    }

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
        if (isvisible)
        {
            // ü���� ǥ���ϴ� UI ������Ʈ
            //healthBar.GetComponent<Slider>().value = curHealth;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Toggle(other);
    }

    private void OnTriggerExit(Collider other)
    {
        Toggle(other);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 10) // ex) HarvestTool layer == 10
        {
            TakeDamage(10);
        }
    }

    private void Toggle(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isvisible = !isvisible;
            //healthBar.SetActive(isvisible);
            //resourceRing.SetActive(isvisible);
        }
    }

    public void TakeDamage(int damage)
    {
        this.curHealth -= damage;

        if(this.curHealth <= 0)
        {
            Destroy(this.gameObject);
            CreateFragments();
        }
    }

    private void CreateFragments()
    {
        int createCounts = Random.Range(4, 7);

        float randomY, randomX, randomZ;

        // ����ȭ�� ������Ʈ ����
        Vector3 spawnPosition;

        for (int i = 0; i < createCounts; i++)
        {
            randomY = 0.5f;
            randomX = Random.Range(0f, 0.5f);
            randomZ = Random.Range(0f, 0.5f);

            // ����ȭ�� ������Ʈ ����
            spawnPosition = transform.position + new Vector3(randomX, randomY, randomZ);
            GameObject fragment = Instantiate(fragmentPrefab, spawnPosition, Quaternion.identity);
            fragment.GetComponent<ResourceFragment>().type = this.type;
        }
    }
}
