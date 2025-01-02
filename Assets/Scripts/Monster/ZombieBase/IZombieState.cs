public interface IZombieState
{
    void EnterState(ZombieBase zombie);
    void UpdateState();
    void ExitState();
}
