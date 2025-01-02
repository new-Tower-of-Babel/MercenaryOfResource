using UnityEngine;

public class IdleState : IdleStateBase
{
    private ZombieBase zombie;

    private bool hasPlayedIdleAnimation = false;    // Check Idle anim execute count

    public override void EnterState(ZombieBase zombie)
    {
        this.zombie = zombie;
        Debug.Log("Entering Idle state");

        if (!hasPlayedIdleAnimation)
        {
            // Idle anim trigger
            zombie.animator.SetTrigger("Idle");
            hasPlayedIdleAnimation = true;
        }
    }

    public override void UpdateState()
    {
        // Change to chase state
        zombie.SwitchState(zombie.chaseState);
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Idle state");
    }
}
