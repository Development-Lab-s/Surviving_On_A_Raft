using UnityEngine;

public class JumpState : IState
{
    readonly EnemyControllFSM e;
    float airTime;               // ������ġ: ���߿� �ʹ� ���� ������ ����
    const float maxAir = 1.0f;   // �ִ� ü�� �ð�

    public JumpState(EnemyControllFSM e) => this.e = e;

    public void Enter()
    {
        e.DoJump();   // rb.velocity.y = data.jumpForceY; + ��Ÿ�� ����
        airTime = 0f;
    }

    public void Tick()
    {
        airTime += Time.deltaTime;
        if (e.IsGrounded() || airTime > maxAir)
        {
            e.ChangeState(new ChaseState(e)); // ����/�ð��ʰ� �� �߰� ����
            return;
        }
    }

    public void FixedTick()
    {
        // ���߿����� ���� �̵� ����(����)
        float dirX = e.DirToPlayerX();
        e.MoveX(dirX * e.data.moveSpeed);
    }

    public void Exit() { }
}
