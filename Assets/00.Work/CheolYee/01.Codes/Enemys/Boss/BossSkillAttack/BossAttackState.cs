using _00.Work.CheolYee._01.Codes.Enemys.FSM;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack
{
    public class BossAttackState : State
    {
        private BossEnemy _boss;
        
        public BossAttackState(Enemy enemy, EnemyStateMachine stateMachine, string boolName) : base(enemy, stateMachine, boolName)
        {
            _boss = enemy as BossEnemy;
        }

        public override void Update()
        {
            base.Update();
            _boss.TryUseSkill();
        }
    }
}