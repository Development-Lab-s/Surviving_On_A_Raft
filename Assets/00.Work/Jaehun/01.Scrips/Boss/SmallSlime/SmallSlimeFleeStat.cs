using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using UnityEngine;

public class SmallSlimeFleeState : EnemyGroundState
{
    private readonly float _preferX;
    private readonly float _deadZone;
    private readonly float _speedScale;

    private readonly SmallSlimeWallProbe _probe; // ����
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
            // �÷��̾� �ݴ� �������� �̵�
            moveDir = Mathf.Sign(dx);
        }

        Enemy.MovementComponent.SetMovement(moveDir * _speedScale);

        // ��Ÿ�� ����
        if (Mathf.Abs(moveDir) > 0.01f && _probe != null)
        {
            if (Time.time >= _nextJumpAllowed && _probe.CheckAhead(Enemy.transform, moveDir, out var hit))
            {
                // ����
                _nextJumpAllowed = Time.time + _jumpCooldown;
                {
                    var rb = Enemy.MovementComponent.RbCompo;

                    // ����ӵ��� �����ϰ�, ������ ����(�Ʒ��� �������� ���̸� 0���� �ø�)
                    if (rb.linearVelocityY < 0f) rb.linearVelocityY = 0f;

                    // SO���� ������ ���: Enemy.enemyData.jumpForce
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
