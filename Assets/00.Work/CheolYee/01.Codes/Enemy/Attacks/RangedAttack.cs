using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemy.Attacks
{
    public class RangedAttack : IAttackBehaviour
    {
        private GameObject _projectilePrefab;
        private float _projectileSpeed;

        public RangedAttack(GameObject projectilePrefab, float projectileSpeed)
        {
            _projectilePrefab = projectilePrefab;
            _projectileSpeed = projectileSpeed;
        }
        
        public void Attack(Enemy enemy)
        {
            if (enemy.targetTrm == null) return;
            
            Debug.Log($"{enemy.name} → 원거리 공격 발사!");
        }
    }
}