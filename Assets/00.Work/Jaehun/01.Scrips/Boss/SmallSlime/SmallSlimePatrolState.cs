using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using UnityEngine;

public class SmallSlimePatrolState : EnemyGroundState
{
    private readonly Vector2 _origin;
    private readonly float _halfWidth;
    private readonly float _pauseTime;

    private int _dir = 1;          // 1=오른쪽, -1=왼쪽
    private float _pauseTimer;

    public SmallSlimePatrolState(Enemy enemy, EnemyStateMachine sm, string boolName,
                                 Vector2 origin, float halfWidth, float pauseTime)
        : base(enemy, sm, boolName)
    {
        _origin = origin;
        _halfWidth = Mathf.Abs(halfWidth);
        _pauseTime = Mathf.Max(0f, pauseTime);
    }

    public override void Enter()
    {
        base.Enter(); // Animator Bool(RUN) = true
        _pauseTimer = 0f;
        // dir 초기화: 현재 위치가 오른쪽이면 왼쪽으로, 반대면 오른쪽으로
        _dir = (Enemy.transform.position.x - _origin.x) >= 0f ? -1 : 1;
    }

    public override void Update()
    {
        base.Update();

        // 끝점 도달 시 잠깐 멈췄다가 반대로
        float leftX = _origin.x - _halfWidth;
        float rightX = _origin.x + _halfWidth;
        float x = Enemy.transform.position.x;

        bool atEdge = (_dir > 0 && x >= rightX - 0.05f) || (_dir < 0 && x <= leftX + 0.05f);
        if (atEdge)
        {
            Enemy.MovementComponent.SetMovement(0);
            _pauseTimer += Time.deltaTime;
            if (_pauseTimer >= _pauseTime)
            {
                _dir *= -1;
                _pauseTimer = 0f;
            }
            return;
        }

        // 이동
        Enemy.MovementComponent.SetMovement(_dir * 1f); // 실제 속도 = moveSpeed * 1
    }

    public override void Exit()
    {
        Enemy.MovementComponent.SetMovement(0);
        base.Exit();
    }
}
