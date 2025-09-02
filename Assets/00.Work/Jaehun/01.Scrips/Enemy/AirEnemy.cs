using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.Resource.SO;
using UnityEngine;

public class AirEnemy : Enemy, IPoolable
{
    public string ItemName => "AirEnemy";

    public GameObject GameObject => gameObject;

    private EnemyStateMachine _stateMachine; //FSM 머신 설정

    // 이게 Idle상태에서 자꾸 멈추는 일이 발생해서 그냥 순찰 기능을 만드는게 좋을거 같아서 만듬.
    [Header("Patrol Settings")]
    [SerializeField] private Transform patrolStart;
    [SerializeField] private Transform patrolEnd;
    [SerializeField] private float patrolDistance = 3f;
    [SerializeField] private float patrolSpeedMul = 0.8f;
    [SerializeField] private float patrolTurnWait = 0.25f; // 끝점에서 살짝 대기
    [SerializeField] private float bobAmplitude = 0.25f;
    [SerializeField] private float bobFrequency = 2.0f;

    protected override void Awake()
    {
        base.Awake();
        _stateMachine = new EnemyStateMachine();

        _stateMachine.AddState(EnemyBehaviourType.Air, new EnemyAirState(this, _stateMachine, "AIR"));

        _stateMachine.AddState(EnemyBehaviourType.Chase, new AirEnemyChaseState(this, _stateMachine, "CHASE"));
        _stateMachine.AddState(EnemyBehaviourType.Attack, new AirEnemyAttackState(this, _stateMachine, "ATTACK"));
        _stateMachine.AddState(EnemyBehaviourType.Idle, new AirEnemyIdleState(this, _stateMachine, "IDLE"));
        _stateMachine.AddState(EnemyBehaviourType.Death, new AirEnemyDeathState(this, _stateMachine, "DEATH"));

        _stateMachine.AddState(EnemyBehaviourType.Patrol,
           new AirEnemyPatrolState(this, _stateMachine, "IDLE", // 애니는 IDLE bool 재사용
                                   patrolStart, patrolEnd,
                                   patrolDistance, patrolSpeedMul,
                                   patrolTurnWait, bobAmplitude, bobFrequency));

        // 시작을 Patrol로
        _stateMachine.Initialize(EnemyBehaviourType.Idle, this);
    }
    public override void SetDead() //죽은 상태로 만들기
    {
        _stateMachine.ChangeState(EnemyBehaviourType.Death);
    }
    private void Update()
    {
        _stateMachine.CurrentState.Update(); //현재 상태에 맞는 업데이트 구문을 실행

        if (targetTrm != null && isDead == false)
        {
            HandleSpriteFlip(targetTrm.position); //타겟 위치에 따라 자동 플립
        }
    }

    public override void AnimationEndTrigger() //애니메이션이 끝났을 떄
    {
        _stateMachine.CurrentState.AnimationEndTrigger(); //애니메이션 종료 시 현재 상태에 맞는 엔드트리거 실행
    }

    public void ResetItem() //풀에서 초기화 될 때
    {
        CanStateChangeable = true;
        isDead = false;
        targetTrm = GameManager.Instance.playerTransform;
        _stateMachine.ChangeState(EnemyBehaviourType.Idle);
        HealthComponent.ResetHealth();
        gameObject.layer = EnemyLayer;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        // 순찰 구간 시각화
        Gizmos.color = Color.yellow;
        Vector2 a, b;
        if (patrolStart && patrolEnd)
        {
            a = patrolStart.position; b = patrolEnd.position;
        }
        else
        {
            var origin = Application.isPlaying
                ? MovementComponent.RbCompo.position
                : (Vector2)transform.position;
            a = origin + Vector2.left * patrolDistance;
            b = origin + Vector2.right * patrolDistance;
        }
        Gizmos.DrawLine(a, b);
        Gizmos.DrawWireSphere(a, 0.1f);
        Gizmos.DrawWireSphere(b, 0.1f);
    }
#endif
}
