using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.Boss.BossSkillAttack;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Boss
{
    public class BossEnemy : GroundEnemy
    {
        [Header("Only Melee Attack Settings")]
        [SerializeField] public DamageCaster damageCaster;
        
        private MeleeAttack _attackBehaviour;
        
        private BossSkillStateMachine _skillStateMachine;
        protected override void Awake()
        {
            base.Awake();
            _attackBehaviour = new MeleeAttack();
            
            _skillStateMachine = new BossSkillStateMachine();
            //_skillStateMachine.AddState(SkillType.Skill1, new 어쩌고SkillState);
        }

        public override void Attack()
        {
            _attackBehaviour?.Attack(this);
        }
    }
}