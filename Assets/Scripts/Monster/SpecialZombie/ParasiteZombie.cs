using UnityEngine;

public class ParasiteZombie : ZombieBase
{
    public GameObject zombieExplosion;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();

        // override 
        Initialize();

        // Initialize states
        this.idleState = new IdleState();
        this.chaseState = new ChaseState();
        this.attackState = new ParasiteAttackState();
        this.hitState = new HitStateBase();
        this.deadState = new ParasiteDeadState();

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
        this.health = 70.0f;
        this.moveSpeed = 3.0f;
        this.attackSpeed = 3.0f;
        this.attackRange = 1.5f;
    }
}
