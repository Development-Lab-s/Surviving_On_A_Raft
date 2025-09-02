using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using UnityEngine;

public class AirEnemyIdleState : EnemyAirState
{
    private const float BobAmplitude = 0.35f;
    private const float BobFrequency = 2.0f;
    private const float PatrolSpeed = 1.5f;    // 왕복 속도 (조절)
    private const float PatrolDist = 3.0f;    // 순찰 반경 (조절)

    private Vector2 _home;
    private float _seed;
    private float _t;  // 시간 누적(왕복용)


    public AirEnemyIdleState(Enemy enemy, EnemyStateMachine sm, string boolName)
        : base(enemy, sm, boolName) { }

    public override void Enter()
    {
        base.Enter(); // Animator Bool(IDLE) = true 세팅됨
                      // 초기화
        var rb = Enemy.MovementComponent.RbCompo;
        _home = rb.position;
        _seed = Random.value * 10f;
        rb.gravityScale = 0f;
        Debug.Log("Idle 진입");
    }

    public override void Update()
    {
        base.Update();
        if (Enemy.isDead) return;

        // 감지되면 추격
        if (Enemy.GetPlayerInRange() != null)
        {
            StateMachine.ChangeState(EnemyBehaviourType.Chase);
            return;
        }

        // 왕복 진행도 (0↔1↔0)
        _t += Time.deltaTime * PatrolSpeed;
        float ping = Mathf.PingPong(_t, 1f);                  // 0~1
        float x = Mathf.Lerp(_home.x - PatrolDist, _home.x + PatrolDist, ping);
        float y = Mathf.Sin((Time.time + _seed) * BobFrequency) * BobAmplitude;

        Vector2 target = new Vector2(x, _home.y + y);
        MoveTowardsSmooth(target, Enemy.enemyData.moveSpeed * 0.8f);
    }

    private void MoveTowardsSmooth(Vector2 target, float maxSpeed)
    {
        var rb = Enemy.MovementComponent.RbCompo;
        Vector2 p = rb.position;
        Vector2 dir = target - p;
        float dist = dir.magnitude;
        if (dist < 0.01f) { rb.linearVelocity = Vector2.zero; return; }
        dir /= dist;
        float lerp = 1f - Mathf.Exp(-8f * Time.deltaTime);
        Vector2 desired = dir * maxSpeed;
        rb.linearVelocity = Vector2.Lerp(rb.linearVelocity, desired, lerp);
    }
}

