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
        Debug.Log("[BossIdleState] Enter");
    }

    public override void Update()
    {
        base.Update();

        var col = Enemy.GetPlayerInRange();
        Debug.Log($"[BossIdleState] Update: detected={(col != null)}");

        if (col != null)
        {
            Debug.Log("[BossIdleState] Player detected -> Chase");
            StateMachine.ChangeState(EnemyBehaviourType.Chase);
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("[BossIdleState] Exit");
    }
}
