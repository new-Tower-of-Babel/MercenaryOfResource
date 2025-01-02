using System.Collections;
using UnityEngine;

public class DeadState : DeadStateBase
{
    private ZombieBase zombie;

    public override void EnterState(ZombieBase zombie)
    {
        this.zombie = zombie;
        Debug.Log("Entering Dead state");

        // Dead anim trigger
        zombie.animator.SetTrigger("Dead");
        zombie.animator.SetInteger("DeadMotion", Random.Range(0, 2));

        // Disable zombie after animation using coroutine
        zombie.StartCoroutine(DisableZombieAfterAnimation());
    }

    public override void UpdateState()
    {
        // Do nothing at dead state
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Dead state");
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
