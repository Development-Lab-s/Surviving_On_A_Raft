using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss.FSM
{
    public class BossSkillNoneState : BossSkillState
    {
        public BossSkillNoneState(BossEnemy bossEnemy, BossSkillStateMachine stateMachine, string skillName, int skillCoolDown) 
            : base(bossEnemy, stateMachine, skillName, skillCoolDown)
        {
        }

        public override void Update()
        {
            base.Update();

            foreach (var VARIABLE in  SkillStateMachine.)
            {
                
            }
        }
    }
}