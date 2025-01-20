using UnityEngine;

public class ParasiteZombie : ZombieBase
{
    public GameObject zombieExplosion;

    public override void Awake()
    {
        base.Awake();

        // Initialize states
        this.idleState = new IdleState();
        this.chaseState = new ChaseState();
        this.attackState = new ParasiteAttackState();
        this.hitState = new HitStateBase();
        this.deadState = new ParasiteDeadState();
    }

    void OnEnable()
    {
        // When object be active
        ResetZombie();
    }

    public override void Start()
    {
        base.Start();

        // override 
        Initialize();

        // Call method by type casting
        if (this.deadState is ParasiteDeadState parasiteDeadState)
        {
            parasiteDeadState.Initialize(zombieExplosion);
        }

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
        this.health = 100.0f;
        this.moveSpeed = 0.6f;
        this.attackSpeed = 3.0f;
        this.attackRange = 1.5f;
    }

    public void ResetZombie()
    {
        base.Initialize();
        Initialize();
    }
}
