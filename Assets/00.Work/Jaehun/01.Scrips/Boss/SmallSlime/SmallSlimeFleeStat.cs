using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using UnityEngine;

public class SmallSlimeFleeState : EnemyGroundState
{
    private readonly float _preferX;
    private readonly float _deadZone;
    private readonly float _speedScale;

    private readonly SmallSlimeWallProbe _probe; // 센서
    private readonly float _jumpCooldown;
    private float _nextJumpAllowed;

    public SmallSlimeFleeState(Enemy enemy, EnemyStateMachine sm, string boolName,
                               float preferDistanceX, float deadZone,
                               float fleeSpeedScale, SmallSlimeWallProbe probe, float jumpCooldown)
        : base(enemy, sm, boolName)
    {
        _preferX = Mathf.Abs(preferDistanceX);
        _deadZone = Mathf.Abs(deadZone);
        _speedScale = Mathf.Max(0.05f, fleeSpeedScale);
        _probe = probe;
        _jumpCooldown = Mathf.Max(0f, jumpCooldown);
        _nextJumpAllowed = 0f;
    }

    public override void Enter()
    {
        base.Enter(); // RUN = true
    }

    public override void Update()
    {
        base.Update();

        if (Enemy.isDead || Enemy.targetTrm == null)
        {
            Enemy.MovementComponent.SetMovement(0);
            return;
        }

        float dx = Enemy.transform.position.x - Enemy.targetTrm.position.x;
        float abs = Mathf.Abs(dx);

        float moveDir = 0f;

        if (abs < _preferX - _deadZone)
        {
            // 플레이어 반대 방향으로 이동
            moveDir = Mathf.Sign(dx);
        }

        Enemy.MovementComponent.SetMovement(moveDir * _speedScale);

        // 쿨타임 적용
        if (Mathf.Abs(moveDir) > 0.01f && _probe != null)
        {
            if (Time.time >= _nextJumpAllowed && _probe.CheckAhead(Enemy.transform, moveDir, out var hit))
            {
                // 점프
                _nextJumpAllowed = Time.time + _jumpCooldown;
                {
                    var rb = Enemy.MovementComponent.RbCompo;

                    // 수평속도는 유지하고, 수직만 리셋(아래로 떨어지는 중이면 0으로 올림)
                    if (rb.linearVelocityY < 0f) rb.linearVelocityY = 0f;

                    // SO에서 점프력 사용: Enemy.enemyData.jumpForce
                    rb.AddForce(Vector2.up * Enemy.enemyData.jumpForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
