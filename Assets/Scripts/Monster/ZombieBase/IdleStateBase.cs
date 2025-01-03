using UnityEngine;

public class IdleStateBase : IZombieState
{
    protected ZombieBase zombie;

    private bool hasPlayedIdleAnimation = false;    // Check Idle anim execute count

    public virtual void EnterState(ZombieBase zombie)
    {
        this.zombie = zombie;

        if (!hasPlayedIdleAnimation)
        {
            // Idle anim trigger
            zombie.animator.SetTrigger("Idle");
            hasPlayedIdleAnimation = true;
        }
    }

    public virtual void ExitState()
    {

    }

    public virtual void UpdateState()
    {
        // Change to chase state
        zombie.SwitchState(zombie.chaseState);
    }
}
