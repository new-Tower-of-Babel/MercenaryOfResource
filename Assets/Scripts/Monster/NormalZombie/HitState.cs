using System.Collections;
using UnityEngine;

public class HitState : HitStateBase
{
    public override void EnterState(ZombieBase zombie)
    {
        base.EnterState(zombie);

        // Hit anim trigger
        zombie.animator.SetTrigger("Hit");
        zombie.StartCoroutine(ZombieHitAnimation());
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    private IEnumerator ZombieHitAnimation()
    {
        // Get current animation state information
        AnimatorStateInfo stateInfo = zombie.animator.GetCurrentAnimatorStateInfo(0);

        // Wait animation time
        yield return new WaitForSeconds(stateInfo.length);
    }
}
