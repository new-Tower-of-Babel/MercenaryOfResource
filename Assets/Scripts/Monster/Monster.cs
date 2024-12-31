using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    [Header("Stat")]
    public int health;
    public float speed;

    [Header("Combat")]
    public int damage;
    public float attackRate;
    public float lastAttackTime;
    public float attackDistance;

    public GameObject player;

    private float playerDistance;
    private float sightAngle = 90f;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        playerDistance = Vector3.Distance(transform.position, player.transform.position);

        if (playerDistance > attackDistance || !IsPlayerInSightAngle())
        {
            agent.isStopped = false;
            NavMeshPath path = new NavMeshPath();
            if (agent.CalculatePath(player.transform.position, path))
            {
                agent.SetDestination(player.transform.position);
            }
        }
        else
        {
            Attack();
        }
    }

    void Attack()
    {
        agent.isStopped = true;
        if (Time.time - lastAttackTime > attackRate)
        {
            lastAttackTime = Time.time;
            Debug.Log("moster do attack");
        }
    }

    bool IsPlayerInSightAngle()
    {
        Vector3 directionToPlayer = player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < sightAngle * 0.5f;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
            Die();
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
