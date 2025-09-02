using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using UnityEngine;

public class AirEnemyChaseState : EnemyAirState
{
    private const float BobAmplitude = 0.25f;
    private const float BobFrequency = 2.0f;
    private float _seed;

    public AirEnemyChaseState(Enemy enemy, EnemyStateMachine sm, string boolName)
        : base(enemy, sm, boolName) { }

    public override void Enter()
    {
        base.Enter(); // Animator Bool(CHASE) = true
        Enemy.MovementComponent.RbCompo.gravityScale = 0f;
        _seed = Random.value * 10f;
        Debug.Log("Chase 진입");
    }

    public override void Update()
    {
        base.Update();
        if (Enemy.isDead) return;

        var player = Enemy.GetPlayerInRange();   // <- 매 프레임 확보
        // 플레이어 못 찾으면 Idle
        if (Enemy.GetPlayerInRange() == null)
        {
            Debug.Log("플레이어를 못찾음.");
            StateMachine.ChangeState(EnemyBehaviourType.Idle);
            return;
        }

        float dist = Vector2.Distance(Enemy.transform.position, Enemy.targetTrm.position);
        bool cooldownOK = (Time.time - Enemy.lastAttackTime) >= Enemy.attackCooldown;

        if (cooldownOK && dist <= Enemy.attackRadius)
        {
            StateMachine.ChangeState(EnemyBehaviourType.Attack);
            return;
        }

        // 접근 + 살짝 상하
        Vector2 target = player.transform.position;
        target.y += Mathf.Sin((Time.time + _seed) * BobFrequency) * BobAmplitude;
        MoveTowardsSmooth(target, Enemy.enemyData.moveSpeed * 1.1f);

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
