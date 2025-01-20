public class ParasiteAttackState : AttackStateBase
{
    public override void EnterState(ZombieBase zombie)
    {
        this.zombie = zombie;
        zombie.Die();
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }

    protected override void Attack()
    {
        // Logic involved with attack
        // Attack anim

        // Decrease player health
    }
}
