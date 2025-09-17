using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using UnityEngine;

public class BossIdleState : EnemyGroundState
{
    public BossIdleState(Enemy enemy, EnemyStateMachine sm, string boolName) : base(enemy, sm, boolName) { }

    public override void Enter()
    {
        base.Enter();
        Enemy.MovementComponent.StopImmediately();
    }

    public override void Update()
    {
        base.Update();

        var col = Enemy.GetPlayerInRange();

        if (col != null)
        {
            StateMachine.ChangeState(EnemyBehaviourType.Chase);
        }
    }
}
