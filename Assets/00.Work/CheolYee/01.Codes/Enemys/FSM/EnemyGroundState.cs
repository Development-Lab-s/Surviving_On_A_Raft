using _00.Work.CheolYee._01.Codes.Enemy;

namespace _00.Work.CheolYee._01.Codes.Enemys.FSM
{
    public abstract class EnemyGroundState : IState
    {
        //땅에 있는 상태만 가져야 할 것들을 뭉탱이로 모아 추상으로 한번 더 묶기
        
        protected EnemyGroundState(Enemy enemy, EnemyStateMachine stateMachine, string boolName) : base(enemy, stateMachine, boolName) {}

        public override void Enter()
        {
            base.Enter();
            Enemy.MovementComponent.IsGround.OnValueChanged += HandleGroundStateChange; //바닥감지로 Air 바꿔야하므로 구독
            
            //맨 처음 기본값 초기화
            HandleGroundStateChange(false, Enemy.MovementComponent.IsGround.Value);
        }

        public override void Update()
        {
            base.Update();

            if (Enemy is GroundEnemy groundEnemy)
            {
                if (groundEnemy.IsWallDetected())
                {
                    StateMachine.ChangeState(EnemyBehaviourType.Jump);
                }
            }
        }

        public override void Exit()
        {
            //나가기 전 구독 해제
            Enemy.MovementComponent.IsGround.OnValueChanged -= HandleGroundStateChange;
            base.Exit();
        }

        private void HandleGroundStateChange(bool prev, bool next) //에너미 바닥감지
        {
            if (next == false) //만약 적이 공중에 있다면
            {
                StateMachine.ChangeState(EnemyBehaviourType.Air); //공중상태로 변경
            }
        }
    }
}