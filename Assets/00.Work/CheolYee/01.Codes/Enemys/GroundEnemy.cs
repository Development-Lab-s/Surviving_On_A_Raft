using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys
{
    public class GroundEnemy : Enemy, IPoolable
    {
        [Header("Ground Settings")]
        [SerializeField] private Transform wallCheck;     // 벽 감지 위치
        [SerializeField] private float wallCheckDistance = 0.5f; //벽 감지 거리
        [SerializeField] private LayerMask whatIsWall; //뭐가 벽이냐?
        [field: SerializeField] public string ItemName { get; private set; } = "GroundEnemy";
        public GameObject GameObject => gameObject; //다른곳에서 오브젝트 쉽게 가져오도록 만들기
        protected EnemyStateMachine StateMachine; //FSM 머신 설정

        public Enemy Enemy { get; private set; }

        public override void SetDead() //죽은 상태로 만들기
        {
            base.SetDead();
            StateMachine.ChangeState(EnemyBehaviourType.Death);
        }

        public override void AnimationEndTrigger() //애니메이션이 끝났을 떄
        {
            StateMachine.CurrentState.AnimationEndTrigger(); //애니메이션 종료 시 현재 상태에 맞는 엔드트리거 실행
        }

        public void ResetItem() //풀에서 초기화 될 때
        {
            isDead = false;
            targetTrm = GameManager.Instance.playerTransform;
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
            StateMachine.AddState(EnemyBehaviourType.Death, new EnemyDeathState(this, StateMachine, "DEATH"));
            //후 초기화
            StateMachine.Initialize(EnemyBehaviourType.Idle, this);
        }

        protected virtual void Update()
        {
            if (targetTrm != null && isDead == false)
            {
                HandleSpriteFlip(targetTrm.position); //움직이는 방향에 따라 자동 플립
            }

            StateMachine.CurrentState.Update(); //현재 상태에 맞는 업데이트 구문을 실행
        }

        public bool IsWallDetected() //전방에 레이를 쏴 감지되었는지 판별
        {
            return Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsWall);
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmosSelected()
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