using System.Collections;
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
        zombie.StartCoroutine(ZombieHitAnimation());
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

    private IEnumerator ZombieHitAnimation()
    {
        // Get current animation state information
        AnimatorStateInfo stateInfo = zombie.animator.GetCurrentAnimatorStateInfo(0);

        // Wait animation time
        yield return new WaitForSeconds(stateInfo.length);
    }
}
