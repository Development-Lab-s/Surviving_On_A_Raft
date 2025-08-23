namespace _00.Work.CheolYee._01.Codes.Enemy.FSM
{
    public class EnemyJumpState : EnemyGroundState
    {
        private readonly GroundEnemy _groundEnemy;
        private bool _hasJumped;
        
        public EnemyJumpState(Enemy enemy, EnemyStateMachine stateMachine, string boolName) : base(enemy, stateMachine, boolName)
        {
            _groundEnemy = enemy as GroundEnemy;
        }

        public override void Enter() //점프 상태에 들어올 시 점프 시작
        {
            base.Enter();

            if (_groundEnemy != null)
            {
                _groundEnemy.MovementComponent.Jump();
                _hasJumped = true;
            }
        }

        public override void Update()
        {
            base.Update();

            if (_hasJumped && _groundEnemy.MovementComponent.IsGround.Value) //점프를 한 후 바닥에 닿았는가 검사
            {
                StateMachine.ChangeState(EnemyBehaviourType.Idle); //닿았다면 아이들 상태로
            }
        }
    }
}