public class AttackStateBase : IZombieState
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

    }

    protected virtual void Attack()
    {
        // Logic involved with attack
        // Attack anim

        // Decrease player health
        PlayDataManager.Instance.characterPlayData.TakeDamage();
    }
}
