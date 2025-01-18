using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public abstract class ZombieBase : MonoBehaviour
{
    public Transform player;    // Player position

    // Zombie Component
    public Animator animator;
    public NavMeshAgent agent;
    public CharacterController controller;

    // Manage Current State
    public IZombieState currentState;

    // States
    public IdleStateBase idleState;
    public ChaseStateBase chaseState;
    public AttackStateBase attackState;
    public HitStateBase hitState;
    public DeadStateBase deadState;

    // Event
    public UnityEvent<GameObject> OnDied;

    // Zombie stat
    public float health;
    public float moveSpeed;
    public float attackSpeed;
    public float attackRange;

    private Vector3 velocity;       // velocity
    private float gravity = -9.8f;  // gravity value

    public bool isDead = false;

    // Get basic component
    public virtual void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        controller = GetComponent<CharacterController>();
    }

    public virtual void Start()
    {
        // Find player position
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    protected void ApplyGravity()
    {
        // Set the gravity value by ground condition
        if (controller.isGrounded)
        {
            velocity.y = 0.0f;
        }
        else
        {
            velocity.y += gravity * Time.fixedDeltaTime;
        }

        if (this.agent.isActiveAndEnabled)
        {
            Vector3 agentVelocity = this.agent.velocity;
            agentVelocity.y = velocity.y;
            controller.Move(agentVelocity * Time.fixedDeltaTime);
        }
        else
        {
            // Controller move process (y axis)
            controller.Move(velocity * Time.fixedDeltaTime);
        }
    }

    public void SwitchState(IZombieState newState)
    {
        // Call current state's ExitState method
        currentState?.ExitState();

        // Change to new state
        currentState = newState;
        currentState.EnterState(this);
    }

    public virtual void Initialize()
    {
        isDead = false;
        SwitchState(idleState);
    }

    public virtual bool IsPlayerInAttackRange()
    {
        return Vector3.Distance(transform.position, player.position) <= attackRange;
    }

    public virtual void TakeDamage(float damage)
    {
        if (isDead) return;

        // Decrease health
        health -= damage;

        // Change hit or dead state depending on health condition
        if (health <= 0)
        {
            isDead = true;
            Die();
        }
        else
        {
            SwitchState(hitState);
        }
    }

    protected virtual void Die()
    {
        SwitchState(deadState);
        OnDied?.Invoke(gameObject);
        ObjectPools.Instance.Despawn(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Projectile>(out var bullet))
        {
            TakeDamage(bullet.weaponStats.damage);
            other.gameObject.SetActive(false);
            ObjectPools.Instance.Despawn(other.gameObject);
        }
    }
}
