using UnityEngine;

public class ChasingState : IState
{
    private Zombie zombie;

    public ChasingState(Zombie zombie)
    {
        this.zombie = zombie;
    }

    public void Enter()
    {

    }

    public void Update()
    {
        zombie.transform.position = Vector3.MoveTowards(zombie.transform.position, zombie.GetPlayer().position, 3f * Time.deltaTime);

        if (Vector3.Distance(zombie.transform.position, zombie.GetPlayer().position) < 1.5f)
        {
            zombie.ChangeState(zombie.attackingState);
        }
    }

    public void Exit()
    {

    }
}
