using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using UnityEngine;

public class AirEnemyPatrolState : EnemyAirState
{
    private readonly Transform _startTf;
    private readonly Transform _endTf;
    private readonly float _patrolDist;
    private readonly float _speedMul;
    private readonly float _turnWait;
    private readonly float _bobA;
    private readonly float _bobF;

    private Vector2 _a;       // 실사용 A점
    private Vector2 _b;       // 실사용 B점
    private Vector2 _goal;    // 현재 목표
    private float _waitT;     // 끝점 대기 타이머
    private float _seed;

    public AirEnemyPatrolState(Enemy enemy, EnemyStateMachine sm, string boolName,
        Transform startTf, Transform endTf, float patrolDist, float speedMul,
        float turnWait, float bobAmplitude, float bobFrequency)
        : base(enemy, sm, boolName)
    {
        _startTf = startTf; _endTf = endTf; _patrolDist = patrolDist;
        _speedMul = speedMul; _turnWait = turnWait;
        _bobA = bobAmplitude; _bobF = bobFrequency;
    }

    public override void Enter()
    {
        base.Enter(); // "IDLE" bool ON (호버 애니)
        var rb = Enemy.MovementComponent.RbCompo;
        rb.gravityScale = 0f;

        // 순찰 구간 계산
        if (_startTf && _endTf)
        {
            _a = _startTf.position;
            _b = _endTf.position;
        }
        else
        {
            // 트랜스폼 미지정 시, 현재 위치 기준 좌우 patrolDist
            var origin = rb.position;
            _a = origin + Vector2.left * _patrolDist;
            _b = origin + Vector2.right * _patrolDist;
        }

        // 가까운 쪽을 먼저 향함
        _goal = (Vector2.Distance(rb.position, _a) <= Vector2.Distance(rb.position, _b)) ? _a : _b;

        _waitT = 0f;
        _seed = Random.value * 10f;
        Debug.Log("[Patrol] Enter");
    }

    public override void Update()
    {
        base.Update();
        if (Enemy.isDead) return;

        // 플레이어 발견 즉시 추격
        var player = Enemy.GetPlayerInRange();
        if (player != null)
        {
            StateMachine.ChangeState(EnemyBehaviourType.Chase);
            return;
        }

        var rb = Enemy.MovementComponent.RbCompo;
        var pos = rb.position;

        // 끝점 도달 → 대기 → 반대편 목표
        if (Vector2.Distance(pos, _goal) <= 0.05f)
        {
            _waitT += Time.deltaTime;
            rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, Vector2.zero, 1f - Mathf.Exp(-8f * Time.deltaTime));
            if (_waitT >= _turnWait)
            {
                _goal = (_goal == _a) ? _b : _a;
                _waitT = 0f;
            }
            return;
        }

        // 목표 + 상하 호버
        var hoverY = Mathf.Sin((Time.time + _seed) * _bobF) * _bobA;
        var target = new Vector2(_goal.x, _goal.y + hoverY);

        MoveTowardsSmooth(target, Enemy.enemyData.moveSpeed * _speedMul);

        // (선택) 스프라이트 플립: vx 기준으로 스케일만 반전
        var vx = rb.linearVelocityX;
        if (Mathf.Abs(vx) > 0.01f)
        {
            var t = Enemy.SpriteRendererComponent.transform;
            var s = t.localScale;
            s.x = vx >= 0 ? Mathf.Abs(s.x) : -Mathf.Abs(s.x);
            t.localScale = s;
        }
    }

    private void MoveTowardsSmooth(Vector2 target, float maxSpeed)
    {
        var rb = Enemy.MovementComponent.RbCompo;
        Vector2 p = rb.position;
        Vector2 dir = target - p;
        float dist = dir.magnitude;
        if (dist < 0.01f) { rb.linearVelocity = Vector2.zero; return; }

        dir /= dist;
        float lerp = 1f - Mathf.Exp(-10f * Time.deltaTime);
        Vector2 desired = dir * maxSpeed;
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, desired, lerp);
    }
}
