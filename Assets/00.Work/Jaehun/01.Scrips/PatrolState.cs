using UnityEngine;

public class PatrolState : IState
{
    readonly EnemyControllFSM e;
    float leftX, rightX;

    public PatrolState(EnemyControllFSM e) => this.e = e;

    public void Enter()
    {
        leftX = e.startPos.x - e.patrolDistance; // ���� ��
        rightX = e.startPos.x + e.patrolDistance; // ������ ��
        if (Mathf.Abs(e.rb.linearVelocity.x) < 0.05f) e.moveDir = 1; // ������ �����ʺ���
    }

    public void Tick()
    {
        // �÷��̾ ���� �ݰ� �ȿ� ������ �߰����� ��ȯ
        if (e.DistanceToPlayer() <= e.data.detectRadius)
        {
            e.ChangeState(new ChaseState(e));
            return;
        }
    }

    public void FixedTick() // �ʴ� ������. �״ϱ� FixedUpdate�� �����ϸ��. ������ �װ� ����.
    {
        e.MoveX(e.moveDir * e.data.moveSpeed); // ������

        float x = e.transform.position.x;     // ���ʹ��� x��ġ�� x��� ������.
        if (x >= rightX) e.moveDir = -1;          // ���� x�� �����ʺ��� ũ�ų� ������� ��������
        else if (x <= leftX) e.moveDir = 1;       // �ݴ�� ����������
    }

    public void Exit() { }

}
