using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NormalZombie : ZombieBase
{
    public override void Awake()
    {
        base.Awake();

        // Initialize states
        this.idleState = new IdleState();
        this.chaseState = new ChaseState();
        this.attackState = new AttackState();
        this.hitState = new HitState();
        this.deadState = new DeadState();
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

    public void ResetZombie()
    {
        base.Initialize();
        Initialize();
    }
}
