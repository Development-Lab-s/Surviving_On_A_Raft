using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using UnityEngine;

public class AirEnemyAttackState : EnemyAirState
{
    // 애니 홀드 시간(애니 길이에 맞춰 조절)
    private const float HoldTime = 0.20f;
    private float _timer;
    private bool _started;

    public AirEnemyAttackState(Enemy enemy, EnemyStateMachine sm, string boolName)
        : base(enemy, sm, boolName) { }

    public override void Enter()
    {
        // 쿨타임 미완료면 즉시 Chase로
        if (Time.time - Enemy.lastAttackTime < Enemy.attackCooldown)
        {
            StateMachine.ChangeState(EnemyBehaviourType.Chase);
            return;
        }

        base.Enter(); // Animator Bool(ATTACK) = true
        Enemy.MovementComponent.RbCompo.linearVelocity = Vector2.zero;
        _timer = HoldTime;
        _started = true;

        // 실제 데미지는 EnemyAnimatorTrigger의 AttackCast() → Enemy.Attack()에서 실행됨
        // (애니 이벤트에 "AttackCast" 연결 필요)
        Debug.Log("Attack 진입");
    }

    public override void Update()
    {
        base.Update();
        if (Enemy.isDead || !_started) return;  // 죽었거나 공격중이면 반환.

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
