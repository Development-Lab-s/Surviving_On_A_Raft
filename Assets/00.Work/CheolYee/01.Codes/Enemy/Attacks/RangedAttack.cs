using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemy.Attacks
{
    public class RangedAttack
    {
        private GameObject _projectilePrefab;
        private float _projectileSpeed;

        public RangedAttack(GameObject projectilePrefab, float projectileSpeed)
        {
            _projectilePrefab = projectilePrefab;
            _projectileSpeed = projectileSpeed;
        }
        
        public void Attack(GroundRangeAttackEnemy enemy)
        {
            if (enemy.targetTrm == null) return;
            
            Debug.Log($"{enemy.name} → 원거리 공격 발사!");
        }

        public void Attack(Players.Player player)
        {
            if (player == null) return;
            
            Debug.Log($"{player.name} → 원거리 공격 발사!");
        }
    }
}