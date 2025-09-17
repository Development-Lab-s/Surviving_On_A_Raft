using _00.Work.CheolYee._01.Codes.Enemys.Attacks;
using _00.Work.CheolYee._01.Codes.Enemys.FSM;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys
{
    public class GroundRangeAttackEnemy : GroundEnemy
    {
        [Header("RangeAttack Settings")]
        public Transform firePos;
        public GameObject projectilePrefab;
        public float projectileSpeed;
        
        private RangedAttack _attackBehaviour;
        protected override void Awake()
        {
            base.Awake();
            StateMachine.AddState(EnemyBehaviourType.Attack, new EnemyAttackState(this, StateMachine, "ATTACK"));
            _attackBehaviour = new RangedAttack(projectilePrefab, projectileSpeed);
        }

        public override void Attack()
        {
            _attackBehaviour.Attack(this);
        }
    }
}