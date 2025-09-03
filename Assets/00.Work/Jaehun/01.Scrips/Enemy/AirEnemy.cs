using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.Resource.SO;
using UnityEngine;

public class AirEnemy : Enemy, IPoolable
{
    public string ItemName => "AirEnemy";

    public GameObject GameObject => gameObject;

    private EnemyStateMachine _stateMachine; //FSM �ӽ� ����

    // �̰� Idle���¿��� �ڲ� ���ߴ� ���� �߻��ؼ� �׳� ���� ����� ����°� ������ ���Ƽ� ����.
    [Header("Patrol Settings")]
    [SerializeField] private Transform patrolStart;
    [SerializeField] private Transform patrolEnd;
    [SerializeField] private float patrolDistance = 3f;
    [SerializeField] private float patrolSpeedMul = 0.8f;
    [SerializeField] private float patrolTurnWait = 0.25f; // �������� ��¦ ���
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
           new AirEnemyPatrolState(this, _stateMachine, "IDLE", // �ִϴ� IDLE bool ����
                                   patrolStart, patrolEnd,
                                   patrolDistance, patrolSpeedMul,
                                   patrolTurnWait, bobAmplitude, bobFrequency));

        // ������ Patrol��
        _stateMachine.Initialize(EnemyBehaviourType.Idle, this);
    }
    public override void SetDead() //���� ���·� �����
    {
        _stateMachine.ChangeState(EnemyBehaviourType.Death);
    }
    private void Update()
    {
        _stateMachine.CurrentState.Update(); //���� ���¿� �´� ������Ʈ ������ ����

        if (targetTrm != null && isDead == false)
        {
            HandleSpriteFlip(targetTrm.position); //Ÿ�� ��ġ�� ���� �ڵ� �ø�
        }
    }

    public override void AnimationEndTrigger() //�ִϸ��̼��� ������ ��
    {
        _stateMachine.CurrentState.AnimationEndTrigger(); //�ִϸ��̼� ���� �� ���� ���¿� �´� ����Ʈ���� ����
    }

    public void ResetItem() //Ǯ���� �ʱ�ȭ �� ��
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
        // ���� ���� �ð�ȭ
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
