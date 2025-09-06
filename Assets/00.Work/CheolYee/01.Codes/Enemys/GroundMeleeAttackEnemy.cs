using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys
{
    public class GroundMeleeAttackEnemy : GroundEnemy
    {
        //근접 공격만 하는 에너미 객체
        [Header("Only Melee Attack Settings")]
        [SerializeField] public DamageCaster damageCaster; //데미지 가하는 컴포넌트
        
        private MeleeAttack _attackBehaviour;
        protected override void Awake()
        {
            base.Awake();
            StateMachine.AddState(EnemyBehaviourType.Attack, new EnemyAttackState(this, StateMachine, "ATTACK"));
            _attackBehaviour = new MeleeAttack();
        }

        public override void Attack()
        {
            _attackBehaviour?.Attack(this);
        }
    }
}