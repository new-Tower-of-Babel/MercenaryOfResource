public class DeadState : IState
{
    private Zombie zombie;

    public DeadState(Zombie zombie)
    {
        this.zombie = zombie;
    }

    public void Enter()
    {
        zombie.GetAnimator().SetBool("IsDead", true);
        zombie.enabled = false;
    }

    public void Update()
    {

    }

    public void Exit()
    {

    }
}
