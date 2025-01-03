public class HitStateBase : IZombieState
{
    protected ZombieBase zombie;

    public virtual void EnterState(ZombieBase zombie)
    {
        this.zombie = zombie;
    }

    public virtual void ExitState()
    {

    }

    public virtual void UpdateState()
    {
        // State change to chase state
        zombie.SwitchState(zombie.chaseState);
    }
}
