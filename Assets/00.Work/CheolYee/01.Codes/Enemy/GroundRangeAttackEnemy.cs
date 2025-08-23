using _00.Work.CheolYee._01.Codes.Enemy.Attacks;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemy
{
    public class GroundRangeAttackEnemy : GroundEnemy
    {
        [Header("RangeAttack Settings")]
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