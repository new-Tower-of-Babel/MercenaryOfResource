public interface IZombieState
{
    void EnterState(Zombie zombie);
    void UpdateState();
    void ExitState();
}
