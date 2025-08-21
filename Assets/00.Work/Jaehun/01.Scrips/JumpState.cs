using UnityEngine;

public class JumpState : IState
{
    readonly EnemyControllFSM e;
    float airTime;               // 안전장치: 공중에 너무 오래 있으면 복귀
    const float maxAir = 1.0f;   // 최대 체공 시간

    public JumpState(EnemyControllFSM e) => this.e = e;

    public void Enter()
    {
        e.DoJump();   // rb.velocity.y = data.jumpForceY; + 쿨타임 설정
        airTime = 0f;
    }

    public void Tick()
    {
        airTime += Time.deltaTime;
        if (e.IsGrounded() || airTime > maxAir)
        {
            e.ChangeState(new ChaseState(e)); // 착지/시간초과 → 추격 복귀
            return;
        }
    }

    public void FixedTick()
    {
        // 공중에서도 수평 이동 유지(간단)
        float dirX = e.DirToPlayerX();
        e.MoveX(dirX * e.data.moveSpeed);
    }

    public void Exit() { }
}
