public class ParasiteAttackState : AttackStateBase
{
    public override void EnterState(ZombieBase zombie)
    {
        this.zombie = zombie;
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        zombie.SwitchState(zombie.deadState);
    }

    protected override void Attack()
    {
        // Logic involved with attack
        // Attack anim

        // Decrease player health
    }
}
