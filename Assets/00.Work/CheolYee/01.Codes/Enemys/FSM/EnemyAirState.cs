using _00.Work.CheolYee._01.Codes.Enemy;

namespace _00.Work.CheolYee._01.Codes.Enemys.FSM
{
    public class EnemyAirState : EnemyState
    {
        public EnemyAirState(Enemy enemy, EnemyStateMachine stateMachine, string boolName) : base(enemy, stateMachine, boolName)
        {
        }

        public override void Update()
        {
            base.Update();
            if (Enemy.MovementComponent.IsGround.Value) //땅에 닿으면
            {
                StateMachine.ChangeState(EnemyBehaviourType.Idle); //아이들로 돌아가기
            }
        }
    }
}