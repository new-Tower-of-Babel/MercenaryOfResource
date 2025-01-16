using System.Collections;
using UnityEngine;

public class DeadState : DeadStateBase
{
    public override void EnterState(ZombieBase zombie)
    {
        base.EnterState(zombie);

        // Dead anim trigger
        zombie.animator.SetTrigger("Dead");
        zombie.animator.SetInteger("DeadMotion", Random.Range(1, 3));

        // Disable zombie after animation using coroutine
        zombie.StartCoroutine(DisableZombieAfterAnimation());
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    private IEnumerator DisableZombieAfterAnimation()
    {
        // Get current animation state information
        AnimatorStateInfo stateInfo = zombie.animator.GetCurrentAnimatorStateInfo(0);

        // Wait animation time
        yield return new WaitForSeconds(stateInfo.length);

        // Disable zombie object
        zombie.gameObject.SetActive(false);
        zombie.Initialize();
    }
}
