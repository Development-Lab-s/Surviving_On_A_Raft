using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using _00.Work.CheolYee._01.Codes.Enemys.FSM.AirEnemy;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys
{
    public class AirEnemy : Enemy, IPoolable
    {

        [field: SerializeField] public string ItemName { get; private set; } = "AirEnemy";
        public GameObject GameObject => gameObject;

        protected EnemyStateMachine StateMachine; //FSM 머신 설정
        protected Enemy enemy;

        protected override void Awake()
        {
            base.Awake();
            StateMachine = new EnemyStateMachine();

            StateMachine.AddState(EnemyBehaviourType.Idle, new AirEnemyIdleState(this, StateMachine, "IDLE"));
            StateMachine.AddState(EnemyBehaviourType.Chase, new AirEnemyChaseState(this, StateMachine, "CHASE"));
            StateMachine.AddState(EnemyBehaviourType.Attack, new AirEnemyAttackState(this, StateMachine, "ATTACK"));
            StateMachine.AddState(EnemyBehaviourType.Death, new EnemyDeathState(this, StateMachine, "DEATH"));

            StateMachine.Initialize(EnemyBehaviourType.Idle, this);
        }
        public override void SetDead() //죽은 상태로 만들기
        {
            StateMachine.ChangeState(EnemyBehaviourType.Death);
        }
        private void Update()
        {
            StateMachine.CurrentState.Update(); //현재 상태에 맞는 업데이트 구문을 실행

            if (targetTrm != null && isDead == false)
            {
                HandleSpriteFlip(targetTrm.position); //움직이는 방향에 따라 자동 플립
            }
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
    }
}
