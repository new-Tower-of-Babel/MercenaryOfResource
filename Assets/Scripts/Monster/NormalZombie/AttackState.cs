﻿using UnityEngine;

public class AttackState : AttackStateBase
{
    private ZombieBase zombie;

    private float attackCooldown = 2.0f;
    private float attackTimer = 0.0f;

    public override void EnterState(ZombieBase zombie)
    {
        this.zombie = zombie;
        Debug.Log("Entering Attack state");
    }

    public override void UpdateState()
    {
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0.0f)
        {
            Attack();
            attackTimer = attackCooldown;   // Reset cooltime
        }

        if (!zombie.IsPlayerInAttackRange())
        {
            zombie.SwitchState(zombie.chaseState);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Attack state");
        zombie.animator.SetBool("Attack", false);
    }

    private void Attack()
    {
        Debug.Log("Zombie attacks player!");
        PlayDataManager.Instance.characterPlayData.nowHealth--;

        // Logic involved with attack
        // Attack anim
        zombie.animator.SetBool("Attack", true);

        // Decrease player health
    }
}
