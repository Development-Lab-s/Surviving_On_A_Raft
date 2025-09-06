using _00.Work.CheolYee._01.Codes.Enemys.FSM;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss.FSM
{
    public class BossAttackState : State
    {
        private BossEnemy _boss;
        
        public BossAttackState(Enemy enemy, EnemyStateMachine stateMachine, string boolName) : base(enemy, stateMachine, boolName)
        {
            _boss = enemy as BossEnemy;
        }

        public override void Enter()
        {
            base.Enter();
            Enemy.MovementComponent.StopImmediately();
            _boss.SkillStateMachine.CurrentState.Enter();
        }

        public override void Update()
        {
            base.Update();
            
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}