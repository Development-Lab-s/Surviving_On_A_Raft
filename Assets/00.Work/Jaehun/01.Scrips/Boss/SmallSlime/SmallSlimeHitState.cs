using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;

public class SmallSlimeHitState : EnemyGroundState
{
    public SmallSlimeHitState(Enemy enemy, EnemyStateMachine sm, string boolName)
        : base(enemy, sm, boolName) { }

    public override void Enter()
    {
        base.Enter(); // Animator Bool(HIT) = true
        Enemy.MovementComponent.StopImmediately(isYAxis: true);
    }

    public override void AnimationEndTrigger()
    {
        base.AnimationEndTrigger();
        // 애니 끝나면 상황에 맞게 복귀
        bool detected = Enemy.GetPlayerInRange() != null;
        StateMachine.ChangeState(detected ? EnemyBehaviourType.Chase : EnemyBehaviourType.Idle);
    }

    public override void Exit()
    {
        Enemy.MovementComponent.SetMovement(0);
        base.Exit();
    }
}
