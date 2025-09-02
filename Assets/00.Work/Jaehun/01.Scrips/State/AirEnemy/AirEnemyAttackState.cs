using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using UnityEngine;

public class AirEnemyAttackState : EnemyAirState
{
    // �ִ� Ȧ�� �ð�(�ִ� ���̿� ���� ����)
    private const float HoldTime = 0.20f;
    private float _timer;
    private bool _started;

    public AirEnemyAttackState(Enemy enemy, EnemyStateMachine sm, string boolName)
        : base(enemy, sm, boolName) { }

    public override void Enter()
    {
        // ��Ÿ�� �̿Ϸ�� ��� Chase��
        if (Time.time - Enemy.lastAttackTime < Enemy.attackCooldown)
        {
            StateMachine.ChangeState(EnemyBehaviourType.Chase);
            return;
        }

        base.Enter(); // Animator Bool(ATTACK) = true
        Enemy.MovementComponent.RbCompo.linearVelocity = Vector2.zero;
        _timer = HoldTime;
        _started = true;

        // ���� �������� EnemyAnimatorTrigger�� AttackCast() �� Enemy.Attack()���� �����
        // (�ִ� �̺�Ʈ�� "AttackCast" ���� �ʿ�)
        Debug.Log("Attack ����");
    }

    public override void Update()
    {
        base.Update();
        if (Enemy.isDead || !_started) return;  // �׾��ų� �������̸� ��ȯ.

        if (IsEndTriggerCall)
        {
            Enemy.lastAttackTime = Time.time;
            StateMachine.ChangeState(EnemyBehaviourType.Chase);
        }
    }

    public override void Exit()
    {
        base.Exit(); // Animator Bool(ATTACK) = false
        Enemy.MovementComponent.RbCompo.linearVelocity = Vector2.zero;
    }
}
