using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Zombie : MonoBehaviour
{
    private IZombieState currentState;  // Manage Current State
    public Transform player;            // Player position
    public Animator animator;           // Animator
    public NavMeshAgent agent;          // NavMeshAgent

    public IdleState idleState;
    public ChaseState chaseState;
    public AttackState attackState;
    public HitState hitState;
    public DeadState deadState;

    public UnityEvent<GameObject> OnDied;

    [Header("Zombie Stat")]
    private float attackRange = 1.5f;
    private float health = 10.0f;

    void Awake()
    {
        animator = GetComponent<Animator>();    // Get animator
        agent = GetComponent<NavMeshAgent>();   // Get NavMeshAgent
    }

    void Start()
    {
        // Find player position
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        // Initialize states
        idleState = new IdleState();
        chaseState = new ChaseState();
        attackState = new AttackState();
        hitState = new HitState();
        deadState = new DeadState();

        SwitchState(idleState); // Set Idle state first time
    }

    void Update()
    {
        // Check state change and apply
        currentState?.UpdateState();
    }

    public void SwitchState(IZombieState newState)
    {
        // Call current state's ExitState method
        currentState?.ExitState();

        // Change to new state
        currentState = newState;
        currentState.EnterState(this);
    }

    public void TakeDamage(float damage)
    {
        // Decrease health
        health -= damage;
        Debug.Log($"Zombie took {damage} damage. Current health: {health}");

        // Change hit or dead state depending on health condition
        if (health <= 0)
        {
            Die();
        }
        else
        {
            SwitchState(hitState);
        }
    }

    public void Die()
    {
        SwitchState(deadState); // Change dead state
        OnDied?.Invoke(gameObject);
        ObjectPool.Instance.Despawn(gameObject);
    }

    public bool IsPlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Projectile>(out var bullet))
        {
            TakeDamage(bullet.weaponStats.damage);
        }
    }
}
