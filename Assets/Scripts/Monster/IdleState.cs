using UnityEngine;

public class IdleState : IState
{
    private Zombie zombie;

    public IdleState(Zombie zombie)
    {
        this.zombie = zombie;
    }

    public void Enter()
    {
        //zombie.GetAnimator().SetFloat("Speed", 0);
    }

    public void Update()
    {
        AnimatorStateInfo stateInfo = zombie.GetAnimator().GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle") && stateInfo.normalizedTime >= 1.0f)
        {
            zombie.ChangeState(zombie.chasingState);
        }

        if (Vector3.Distance(zombie.transform.position, zombie.GetPlayer().position) < 10f)
        {
            zombie.ChangeState(zombie.chasingState);
        }
    }

    public void Exit()
    {

    }
}
