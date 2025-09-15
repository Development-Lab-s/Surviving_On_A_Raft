using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using _00.Work.Jaehun._01.Scrips.Boss;
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
            // Ÿ���� �Ҿ����� Idle
            Debug.Log("[BossChaseState] No player in detect radius -> Idle");
            Enemy.MovementComponent.StopImmediately();
            StateMachine.ChangeState(EnemyBehaviourType.Idle);
            return;
        }

        // �¿� �̵�(�����ϰ� X ���⸸)
        var t = targetCol.transform;
        float dx = t.position.x - Enemy.transform.position.x;
        float dir = Mathf.Sign(dx);
        Enemy.MovementComponent.SetMovement(dir);

        Debug.Log($"[BossChaseState] Move toward player: dx={dx:F2}, dir={dir}, attackRadius={Enemy.attackRadius}");

        // ���� ��Ÿ��� ���԰�, ������ OK�̸�, ��� ������ ��ų�� ������ Attack����
        bool inAttackRange = Mathf.Abs(dx) <= Enemy.attackRadius;
        bool globalReady = (Enemy is BossSlime b) ? b.IsGlobalSkillReady() : true;
        Debug.Log($"[BossChaseState] inAttackRange={inAttackRange}, globalReady={globalReady}");


        if (inAttackRange && globalReady)
        {
            // Attack ���°� ���� ���� ������ ��ų�� ������ ��� �ٽ� Chase�� ���ư�����
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
