using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] protected MonsterData data;
    [SerializeField] protected GameObject player;
    protected Rigidbody rb;
    protected Animator animator;
    protected int curHealth;
    public int CurHealth
    {
        get { return curHealth; }
        set 
        { 
            curHealth = value;
            if (curHealth < 0) Die();
        }
    }
    protected float playerDistance;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        CurHealth = data.health;
    }

    public void Start()
    {
        //player = GameManager.Instance.Player.gameObject; 

    }

    public void Attack()
    {
        float distance = Vector3.Distance(player.transform.position, transform.position);
        if (distance <= data.attackDistance)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void MoveToPlayer()
    {
        Vector3 dir = (player.transform.position - transform.position).normalized;
        rb.velocity = dir * data.moveSpeed;
        transform.forward = new Vector3(dir.x, 0, dir.z).normalized;
    }
    public void TakeDamage(int damage)
    {
        CurHealth -= damage;
    }


    public void Die()
    {
        animator.SetTrigger("Die");
        gameObject.SetActive(false);
    }
}
