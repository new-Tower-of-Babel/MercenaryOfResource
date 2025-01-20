using System.Collections;
using UnityEngine;

public class WhiteClownZombie : ZombieBase
{
    public float healingAmount = 10f;
    public float healingRadius = 5f;
    public float healingInterval = 10f;

    public override void Awake()
    {
        base.Awake();

        // Initialize states
        this.idleState = new IdleState();
        this.chaseState = new ChaseState();
        this.attackState = new AttackState();
        this.hitState = new HitStateBase();
        this.deadState = new DeadState();
    }

    void OnEnable()
    {
        // When object be active
        ResetZombie();
        StartCoroutine(HealingCoroutine());
    }

    public override void Start()
    {
        base.Start();

        // override 
        Initialize();

        // Set Idle state first time
        SwitchState(idleState);
    }

    void Update()
    {
        // Apply gravity => parent base method
        ApplyGravity();

        // Check state change and apply
        currentState?.UpdateState();
    }

    public override void Initialize()
    {
        this.health = 70.0f;
        this.moveSpeed = 0.4f;
        this.attackSpeed = 3.0f;
        this.attackRange = 1.5f;
    }

    void OnDisable()
    {
        StopCoroutine(HealingCoroutine());
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (other.CompareTag("Zombie"))
        {
            Heal(other.gameObject);
        }
    }

    private IEnumerator HealingCoroutine()
    {
        while (true)
        {
            // Find object where in heal range
            Collider[] colliders = Physics.OverlapSphere(transform.position, healingRadius);

            foreach (Collider col in colliders)
            {
                if (col.CompareTag("Zombie"))
                {
                    Heal(col.gameObject);
                    ParticleManager.Instance.PlayParticleEffect("HealEffect", col.gameObject.transform.position);
                }
            }

            // Wait heal interval
            yield return new WaitForSeconds(healingInterval);
        }
    }

    private void Heal(GameObject target)
    {
        ZombieBase targetZombie = target.GetComponent<ZombieBase>();
        if (targetZombie != null)
        {
            targetZombie.health += healingAmount;

            // 추후 좀비의 최대 체력을 초과하여 회복하는 것 방지
            //targetZombie.health = Mathf.Min(targetZombie.health + healingAmount, targetZombie.maxHealth);
        }
    }

    public void ResetZombie()
    {
        base.Initialize();
        Initialize();
    }
}
