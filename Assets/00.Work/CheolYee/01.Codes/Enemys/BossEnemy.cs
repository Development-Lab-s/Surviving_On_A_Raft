using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys
{
    public class BossEnemy : GroundEnemy
    {
        [Header("Only Melee Attack Settings")]
        [SerializeField] public DamageCaster damageCaster;
        
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