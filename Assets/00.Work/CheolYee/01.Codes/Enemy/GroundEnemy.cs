using _00.Work.CheolYee._01.Codes.Enemy.FSM;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemy
{
    public class GroundEnemy : Enemy, IPoolable
    {
        //풀링에 저장될 이름
        [field: SerializeField] public string ItemName { get; private set; } = "GroundEnemy";
        
        [Header("Ground Settings")]
        [SerializeField] private float jumpForce = 10f;   // 점프 힘
        [SerializeField] private Transform wallCheck;     // 벽 감지 위치
        [SerializeField] private float wallCheckDistance = 0.5f;
        [SerializeField] private LayerMask whatIsWall;
        public GameObject GameObject => gameObject; //다른곳에서 오브젝트 쉽게 가져오도록 만들기
        public EnemyStateMachine StateMachine;
        public override void AnimationEndTrigger() //애니메이션이 끝났을 떄
        {
            StateMachine.CurrentState.AnimationEndTrigger(); //애니메이션 종료 시 현재 상태에 맞는 엔드트리거 실행
        }
        
        public void ResetItem() //풀링 끝나고 초기화 될 때
        {
            CanStateChangeable = true;
            isDead = false;
            targetTrm = null;
            StateMachine.ChangeState(EnemyBehaviourType.Idle);
            HealthComponent.ResetHealth();
            gameObject.layer = EnemyLayer;
        }

        protected override void Awake()
        {
            base.Awake();

            StateMachine = new EnemyStateMachine(); //처음 생성되었을 시 설정해준다
            
            //모든 상태 추가
            StateMachine.AddState(EnemyBehaviourType.Air, new EnemyAirState(this, StateMachine, "AIR"));
            StateMachine.AddState(EnemyBehaviourType.Idle, new EnemyIdleState(this, StateMachine, "IDLE"));
            StateMachine.AddState(EnemyBehaviourType.Chase, new EnemyChaseState(this, StateMachine, "CHASE"));
            StateMachine.AddState(EnemyBehaviourType.Jump, new EnemyJumpState(this, StateMachine, "JUMP"));
            StateMachine.AddState(EnemyBehaviourType.Attack, new EnemyAttackState(this, StateMachine, "ATTACK"));
            StateMachine.AddState(EnemyBehaviourType.Death, new EnemyDeathState(this, StateMachine, "DEATH"));
            
            //후 초기화
            StateMachine.Initialize(EnemyBehaviourType.Idle, this);
        }

        private void Update()
        {
            StateMachine.CurrentState.Update(); //현재 상태에 맞는 업데이트 구문을 실행

            if (targetTrm != null && isDead == false)
            {
                HandleSpriteFlip(targetTrm.position); //타겟 위치에 따라 자동 플립
            }
        }

        public bool IsWallDetected() //전방에 레이를 쏴 감지되었는지 판별
        {
            return Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsWall);
        }

        public void Jump() //점프 메서드
        {
            MovementComponent.RbCompo.linearVelocityY = 0; // 기존 Y 속도 초기화
            MovementComponent.RbCompo.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
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