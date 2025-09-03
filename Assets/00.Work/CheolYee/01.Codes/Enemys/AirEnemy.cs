using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using _00.Work.CheolYee._01.Codes.Enemys.FSM.AirEnemy;
using _00.Work.CheolYee._01.Codes.Managers;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys
{
    public class AirEnemy : Enemy, IPoolable
    {
        public string ItemName => "AirEnemy";

        public GameObject GameObject => gameObject;

        private EnemyStateMachine _stateMachine; //FSM 머신 설정

        protected override void Awake()
        {
            base.Awake();
            _stateMachine = new EnemyStateMachine();

            _stateMachine.AddState(EnemyBehaviourType.Idle, new AirEnemyIdleState(this, _stateMachine,"IDLE"));
            _stateMachine.AddState(EnemyBehaviourType.Chase, new AirEnemyChaseState(this, _stateMachine, "CHASE"));
            _stateMachine.AddState(EnemyBehaviourType.Attack, new AirEnemyAttackState(this, _stateMachine, "ATTACK"));
            _stateMachine.AddState(EnemyBehaviourType.Death, new EnemyDeathState(this, _stateMachine, "DEATH"));

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
    }
}
