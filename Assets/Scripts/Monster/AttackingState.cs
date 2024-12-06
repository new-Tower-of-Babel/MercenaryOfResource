using UnityEngine;

public class AttackingState : IState
{
    private Zombie zombie;

    private float attackDelay = 1f;
    private float attackTime = 0f;

    public AttackingState(Zombie fsm)
    {
        this.zombie = fsm;
    }

    public void Enter()
    {
        zombie.GetAnimator().SetTrigger("Attack");

        attackTime = 0f;
    }

    public void Update()
    {
        if (attackTime < attackDelay)
        {
            attackTime += Time.deltaTime;
        }
        else
        {
            if (Vector3.Distance(zombie.transform.position, zombie.GetPlayer().position) > 1.5f)
            {
                zombie.ChangeState(zombie.chasingState);
            }
        }
    }

    public void Exit()
    {

    }
}
