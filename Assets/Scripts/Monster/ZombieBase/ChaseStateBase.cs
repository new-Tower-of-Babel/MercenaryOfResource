public class ChaseStateBase : IZombieState
{
    protected ZombieBase zombie;

    public virtual void EnterState(ZombieBase zombie)
    {
        this.zombie = zombie;

        // Chase anim start (Walk or run)
        zombie.agent.speed = zombie.moveSpeed;
        zombie.animator.SetBool("Chase", true);
    }

    public virtual void ExitState()
    {
        zombie.agent.ResetPath();
        zombie.animator.SetBool("Chase", false);
    }

    public virtual void UpdateState()
    {
        // Chase player code
        zombie.agent.SetDestination(zombie.player.position);

        // Change state depending on condition
        if (zombie.IsPlayerInAttackRange())
        {
            zombie.SwitchState(zombie.attackState); // Change to attack state
        }
    }
}
