using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys
{
    public class AirMeleeAttackEnemy : AirEnemy
    {
        // 원거리 몬스터 전용 공격.
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
