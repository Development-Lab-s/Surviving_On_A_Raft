using _00.Work.CheolYee._01.Codes.Enemy.FSM;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemy
{
    public class GroundEnemy : Enemy, IPoolable
    {
        [Header("Ground Settings")]
        [SerializeField] private Transform wallCheck;     // 벽 감지 위치
        [SerializeField] private float wallCheckDistance = 0.5f; //벽 감지 거리
        [SerializeField] private LayerMask whatIsWall; //뭐가 벽이냐?
        [field: SerializeField] public string ItemName { get; private set; } = "GroundEnemy";
        public GameObject GameObject => gameObject; //다른곳에서 오브젝트 쉽게 가져오도록 만들기
        private EnemyStateMachine _stateMachine; //FSM 머신 설정
        public override void SetDead() //죽은 상태로 만들기
        {
            _stateMachine.ChangeState(EnemyBehaviourType.Death);
        }

        public override void AnimationEndTrigger() //애니메이션이 끝났을 떄
        {
            _stateMachine.CurrentState.AnimationEndTrigger(); //애니메이션 종료 시 현재 상태에 맞는 엔드트리거 실행
        }
        
        public void ResetItem() //풀에서 초기화 될 때
        {
            CanStateChangeable = true;
            isDead = false;
            targetTrm = null;
            _stateMachine.ChangeState(EnemyBehaviourType.Idle);
            HealthComponent.ResetHealth();
            gameObject.layer = EnemyLayer;
        }

        protected override void Awake()
        {
            base.Awake();

            _stateMachine = new EnemyStateMachine(); //처음 생성되었을 시 설정해준다
            
            //모든 상태 추가
            _stateMachine.AddState(EnemyBehaviourType.Air, new EnemyAirState(this, _stateMachine, "AIR"));
            _stateMachine.AddState(EnemyBehaviourType.Idle, new EnemyIdleState(this, _stateMachine, "IDLE"));
            _stateMachine.AddState(EnemyBehaviourType.Chase, new EnemyChaseState(this, _stateMachine, "CHASE"));
            _stateMachine.AddState(EnemyBehaviourType.Jump, new EnemyJumpState(this, _stateMachine, "JUMP"));
            _stateMachine.AddState(EnemyBehaviourType.Attack, new EnemyAttackState(this, _stateMachine, "ATTACK"));
            _stateMachine.AddState(EnemyBehaviourType.Death, new EnemyDeathState(this, _stateMachine, "DEATH"));
            //후 초기화
            _stateMachine.Initialize(EnemyBehaviourType.Idle, this);
        }

        private void Update()
        {
            _stateMachine.CurrentState.Update(); //현재 상태에 맞는 업데이트 구문을 실행

            if (targetTrm != null && isDead == false)
            {
                HandleSpriteFlip(targetTrm.position); //타겟 위치에 따라 자동 플립
            }
        }

        public bool IsWallDetected() //전방에 레이를 쏴 감지되었는지 판별
        {
            return Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsWall);
        }
        
        #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            if (wallCheck != null)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(wallCheck.position, wallCheck.position + transform.right * wallCheckDistance);
            }
        }
#endif
    }
}