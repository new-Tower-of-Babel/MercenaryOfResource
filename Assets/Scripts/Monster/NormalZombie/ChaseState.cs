using UnityEngine;
using UnityEngine.AI;

public class ChaseState : ChaseStateBase
{
    private ZombieBase zombie;

    private float speed = 1.0f;

    public void EnterState(ZombieBase zombie)
    {
        this.zombie = zombie;
        Debug.Log("Entering Chase state");

        // Chase anim start (Walk or run)
        zombie.animator.SetBool("Walk", true);
    }

    public void UpdateState()
    {
        // Chase player code
        zombie.agent.SetDestination(zombie.player.position);

        // Change state depending on condition
        if (zombie.IsPlayerInAttackRange())
        {
            zombie.SwitchState(zombie.attackState); // Change to attack state
        }
    }

    public void ExitState()
    {
        Debug.Log("Exiting Chase state");
        zombie.agent.ResetPath();
        zombie.animator.SetBool("Walk", false);
    }
}
