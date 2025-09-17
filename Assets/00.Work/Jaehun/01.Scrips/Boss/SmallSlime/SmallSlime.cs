using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using UnityEngine;
using UnityEngine.InputSystem;

public class SmallSlime : GroundEnemy
{
    [Header("Patrol Settings")]
    [SerializeField] private float patrolHalfWidth = 4f;
    [SerializeField] private float patrolPauseTime = 0.3f;

    [Header("Flee Settings")]
    [SerializeField] private float preferDistanceX = 6f;
    [SerializeField] private float deadZone = 0.4f;
    [SerializeField] private float fleeSpeedScale = 1.0f;

    [Header("Jump Settings")]
    [SerializeField] private SmallSlimeWallProbe wallProbe;
    [SerializeField] private float jumpCooldown = 0.35f;    // <- 점프 남발 방지

    [Header("Phase2 Boss Spawn")]
    [SerializeField] private GameObject bossSlimePrefab;     // 2페이즈 보스 프리팹
    [SerializeField] private Transform bossSpawnPoint;       // 없으면 본인 위치 사용
    [SerializeField] private bool inheritFacing = true;      // 스몰 슬라임의 좌우 방향 계승 여부


    private bool _bossSpawned;  // 소환은 한번만 하기 위함.
    private Vector2 _spawnPos;

    protected override void Awake()
    {
        base.Awake();

        _spawnPos = transform.position;

        StateMachine = new EnemyStateMachine();
        
        StateMachine.AddState(EnemyBehaviourType.Air, new EnemyAirState(this, StateMachine, "AIR"));
        StateMachine.AddState(EnemyBehaviourType.Jump, new EnemyJumpState(this, StateMachine, "JUMP"));

        StateMachine.AddState(EnemyBehaviourType.Idle,
            new SmallSlimePatrolState(this, StateMachine, "RUN", _spawnPos, patrolHalfWidth, patrolPauseTime));

        StateMachine.AddState(EnemyBehaviourType.Chase,
            new SmallSlimeFleeState(this, StateMachine, "RUN",
                                    preferDistanceX, deadZone, fleeSpeedScale,
                                    wallProbe, jumpCooldown));

        StateMachine.AddState(EnemyBehaviourType.Attack,
            new SmallSlimeHitState(this, StateMachine, "HIT"));

        StateMachine.AddState(EnemyBehaviourType.Death,
            new SmallSlimeDeathState(this, StateMachine, "DEATH"));

        StateMachine.Initialize(EnemyBehaviourType.Idle, this);

        HealthComponent.onHit.AddListener(OnHit);
        HealthComponent.onDeath.AddListener(OnDeath);
    }


    protected override void Update()
    {
        // 바라보는 방향 자동 플립(공용 GroundEnemy가 velocity로 처리)
        base.Update();

        // 순찰 <-> 도망 전이
        if (!isDead)
        {
            bool detected = GetPlayerInRange() != null;

            if (detected && StateMachine.CurrentState is SmallSlimePatrolState)
                StateMachine.ChangeState(EnemyBehaviourType.Chase);
            else if (!detected && StateMachine.CurrentState is SmallSlimeFleeState)
                StateMachine.ChangeState(EnemyBehaviourType.Idle);
        }

        if (Keyboard.current.iKey.wasPressedThisFrame)
        {
            SetDead(); // -> Death 상태 진입
        }
    }

    private void OnHit()
    {
        if (isDead) return;
        StateMachine.ChangeState(EnemyBehaviourType.Attack); // Hit 상태로
    }

    private void OnDeath()
    {
        StateMachine.ChangeState(EnemyBehaviourType.Death);
    }

    public override void AnimationEndTrigger()
    {
        StateMachine.CurrentState.AnimationEndTrigger();
    }

    public override void SetDead()
    {
        StateMachine.ChangeState(EnemyBehaviourType.Death);
    }

    public void AnimEvent_SpawnBossAndDespawnSelf()
    {
        if (bossSlimePrefab == null)
        {
        }

        // 1. 보스 소환
        if (bossSlimePrefab != null)
        {
            Vector3 pos = bossSpawnPoint ? bossSpawnPoint.position : transform.position;

            // 회전/방향 계승(프로젝트 스타일에 맞춰 간단히 처리)
            Quaternion rot = transform.rotation; // 보통 2D는 그대로 사용
            var boss = Instantiate(bossSlimePrefab, pos, rot);

            // (선택사항임) 좌우 방향 계승: flipX 또는 localScale.x 부호를 복사
            if (inheritFacing && SpriteRendererComponent != null)
            {
                bool flipped = SpriteRendererComponent.flipX;
                var bossSR = boss.GetComponentInChildren<SpriteRenderer>();
                if (bossSR != null) bossSR.flipX = flipped;
                else boss.transform.localScale = new Vector3(
                    flipped ? -Mathf.Abs(boss.transform.localScale.x) : Mathf.Abs(boss.transform.localScale.x),
                    boss.transform.localScale.y,
                    boss.transform.localScale.z
                );
            }

            // (선택) 타겟/쿨다운 등 초기값 전달
            if (boss.TryGetComponent<Enemy>(out var bossEnemy))
            {
                bossEnemy.targetTrm = this.targetTrm;
                bossEnemy.lastAttackTime = Time.time; // 스폰 직후 난사 방지 등
            }
        }

        // 2) 본인 제거(풀을 쓰지 않는다면 Destroy)
        Destroy(gameObject);

        // ※ 풀 매니저를 쓴다면 여기에 맞춰 교체:
        // PoolManager.Instance?.Despawn(gameObject);
    }
}
