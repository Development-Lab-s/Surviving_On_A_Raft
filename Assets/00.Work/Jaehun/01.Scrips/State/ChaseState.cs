using UnityEngine;

internal class ChaseState : IState
{
    readonly EnemyControllFSM e;
    const float chaseMul = 1.2f; // 추격 시 약간 가속(원하면 1.0f)

    public ChaseState(EnemyControllFSM e) => this.e = e;

    public void Enter()
    {
        /*e.SetRun(true);*/
        if (e.anim) e.anim.SetBool("HashIsHunting", true); // 추격시작
    }

    public void Tick()
    {
        float dist = e.DistanceToPlayer();

        if (dist > e.data.detectRadius)
        {
            e.ChangeState(new PatrolState(e)); // 잃어버리면 순찰 복귀
            return;
        }

        if (dist <= e.data.attackRange)
        {
            e.ChangeState(new AttackState(e)); // 사거리면 공격
            return;
        }

        // 접지 + 점프쿨 끝 + 전방 장애물 → 점프
        if (e.IsGrounded() && e.jumpCooldownTimer <= 0f && e.HasObstacleAhead())
        {
            e.ChangeState(new JumpState(e));
        }
    }

    public void FixedTick()
    {
        float dx = e.DirToPlayerX();
        float dirX = Mathf.Abs(dx) < 0.1f ? 0 : Mathf.Sign(dx);

        e.MoveX(dirX * e.data.moveSpeed * chaseMul);

        if (dirX != 0)
            e.moveDir = (int)dirX;

    }

    public void Exit()
    {
        /*e.SetRun(false);*/
        if (e.anim) e.anim.SetBool("HashIsHunting", false);
    }
}