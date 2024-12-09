using UnityEngine;

public class AttackState : IZombieState
{
    private Zombie zombie;

    private float attackCooldown = 2.0f;
    private float attackTimer = 0.0f;

    public void EnterState(Zombie zombie)
    {
        this.zombie = zombie;
        Debug.Log("Entering Attack state");
    }

    public void UpdateState()
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

    public void ExitState()
    {
        Debug.Log("Exiting Attack state");
        zombie.animator.SetBool("Attack", false);
    }

    private void Attack()
    {
        Debug.Log("Zombie attacks player!");

        // Logic involved with attack
        // Attack anim
        zombie.animator.SetBool("Attack", true);

        // Decrease player health
    }
}
