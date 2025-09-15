using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using UnityEngine;

public class BossChaseState : EnemyGroundState
{
    public BossChaseState(Enemy enemy, EnemyStateMachine sm, string boolName) : base(enemy, sm, boolName) { }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("[BossChaseState] Enter");
    }

    public override void Update()
    {
        base.Update();

        var targetCol = Enemy.GetPlayerInRange();
        if (targetCol == null)
        {
            // 타겟을 잃었으면 Idle
            Debug.Log("[BossChaseState] No player in detect radius -> Idle");
            Enemy.MovementComponent.StopImmediately();
            StateMachine.ChangeState(EnemyBehaviourType.Idle);
            return;
        }

        // 좌우 이동(간단하게 X 방향만)
        var t = targetCol.transform;
        float dx = t.position.x - Enemy.transform.position.x;
        float dir = Mathf.Sign(dx);
        Enemy.MovementComponent.SetMovement(dir);

        Debug.Log($"[BossChaseState] Move toward player: dx={dx:F2}, dir={dir}, attackRadius={Enemy.attackRadius}");

        // 공격 사거리에 들어왔고, 전역쿨 OK이며, 사용 가능한 스킬이 있으면 Attack으로
        bool inAttackRange = Mathf.Abs(dx) <= Enemy.attackRadius;
        bool globalReady = (Enemy is BossSlime b) ? b.IsGlobalSkillReady() : true;
        Debug.Log($"[BossChaseState] inAttackRange={inAttackRange}, globalReady={globalReady}");


        if (inAttackRange && globalReady)
        {
            // Attack 상태가 실제 선택 가능한 스킬이 없으면 즉시 다시 Chase로 돌아가도록
            StateMachine.ChangeState(EnemyBehaviourType.Attack);
            Debug.Log("[BossChaseState] -> Attack");
        }
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("[BossChaseState] Exit");
    }
}
