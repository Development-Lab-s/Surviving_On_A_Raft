using _00.Work.CheolYee._01.Codes.Enemys;
using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemy
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
            _attackBehaviour = new MeleeAttack();
        }

        public override void Attack()
        {
            _attackBehaviour?.Attack(this);
        }
    }
}