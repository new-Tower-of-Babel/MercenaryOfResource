using UnityEngine;

public class HitState : IZombieState
{
    private Zombie zombie;

    public void EnterState(Zombie zombie)
    {
        this.zombie = zombie;
        Debug.Log("Entering Hit state");

        // Hit anim trigger
        zombie.animator.SetTrigger("Hit");
    }

    public void UpdateState()
    {
        // State change
        zombie.SwitchState(zombie.chaseState);
    }

    public void ExitState()
    {
        Debug.Log("Exiting Hit state");
    }
}
