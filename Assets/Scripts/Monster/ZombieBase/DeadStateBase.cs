﻿public class DeadStateBase : IZombieState
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
        // Do nothing at dead state
    }
}
