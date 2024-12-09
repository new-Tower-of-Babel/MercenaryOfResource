using UnityEngine;

public class IdleState : IZombieState
{
    private Zombie zombie;

    private bool hasPlayedIdleAnimation = false;    // Check Idle anim execute count

    public void EnterState(Zombie zombie)
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

    public void UpdateState()
    {
        // Change to chase state
        zombie.SwitchState(zombie.chaseState);
    }

    public void ExitState()
    {
        Debug.Log("Exiting Idle state");
    }
}
