using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys
{
    public class AirRangeAttackEnemy : AirEnemy
    {
        [Header("RangeAttack Settings")]
        public Transform firePos;
        public GameObject projectilePrefab;
        public float projectileSpeed;
        
        private RangedAttack _attackBehaviour;
        protected override void Awake()
        {
            base.Awake();
            _attackBehaviour = new RangedAttack(projectilePrefab, projectileSpeed);
        }

        public override void Attack()
        {
            _attackBehaviour.Attack(this);
        }
    }
}