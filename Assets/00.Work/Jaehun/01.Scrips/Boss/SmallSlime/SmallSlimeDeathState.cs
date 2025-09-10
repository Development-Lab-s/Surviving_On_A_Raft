using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;

public class SmallSlimeDeathState : EnemyGroundState
{
    public SmallSlimeDeathState(Enemy enemy, EnemyStateMachine sm, string boolName)
        : base(enemy, sm, boolName) { }

    public override void Enter()
    {
        base.Enter(); // Animator Bool(DEATH) = true
        Enemy.isDead = true;
        Enemy.MovementComponent.StopImmediately(isYAxis: true);
        Enemy.onDeath?.Invoke();
    }

}
