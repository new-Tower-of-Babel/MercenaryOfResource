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
        // State change
        if (zombie.IsPlayerInAttackRange())
        {
            zombie.SwitchState(zombie.attackState); // Change to attack state
        }
        else
        {
            zombie.SwitchState(zombie.chaseState);
        }
    }
}
