using UnityEngine;

public class AttackState : AttackStateBase
{
    private float attackCooldown = 2.0f;
    private float attackTimer = 0.0f;

    public override void EnterState(ZombieBase zombie)
    {
        base.EnterState(zombie);
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
        base.ExitState();
        zombie.animator.SetBool("Attack", false);
    }

    protected override void Attack()
    {
        Debug.Log("Zombie attacks player!");

        // Logic involved with attack
        // Attack anim
        zombie.animator.SetBool("Attack", true);

        // Decrease player health
        PlayDataManager.Instance.characterPlayData.nowHealth--;
    }
}
