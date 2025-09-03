using UnityEngine;

public class PatrolState : IState
{
    readonly EnemyControllFSM e;
    float leftX, rightX;

    public PatrolState(EnemyControllFSM e) => this.e = e;

    public void Enter()
    {
        leftX = e.startPos.x - e.patrolDistance; // 왼쪽 끝
        rightX = e.startPos.x + e.patrolDistance; // 오른쪽 끝
        if (Mathf.Abs(e.rb.linearVelocity.x) < 0.05f) e.moveDir = 1; // 정지면 오른쪽부터
    }

    public void Tick()
    {
        // 플레이어가 감지 반경 안에 들어오면 추격으로 전환
        if (e.DistanceToPlayer() <= e.data.detectRadius)
        {
            e.ChangeState(new ChaseState(e));
            return;
        }
    }

    public void FixedTick() // 정해진 시간당 움직임. 그니까 FixedUpdate라 생각하면됨. 실제로 그게 맞음.
    {
        e.MoveX(e.moveDir * e.data.moveSpeed); // 움직임

        float x = e.transform.position.x;     // 에너미의 x위치를 x라고 저장함.
        if (x >= rightX) e.moveDir = -1;          // 이제 x가 오른쪽보다 크거나 같을경우 왼쪽으로
        else if (x <= leftX) e.moveDir = 1;       // 반대는 오른쪽으로
    }

    public void Exit() { }

}
