using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss
{
    public class BossEnemy : GroundEnemy
    {
        [Header("Only Melee Attack Settings")]
        [SerializeField] public DamageCaster damageCaster;
        
        public BossSkillStateMachine SkillStateMachine { get; private set; }
        protected override void Awake()
        {
            base.Awake();
            SkillStateMachine = new BossSkillStateMachine();
            //_skillStateMachine.AddState(SkillType.Skill1, new 어쩌고SkillState);
        }

        public override void Attack()
        {
        }
    }
}