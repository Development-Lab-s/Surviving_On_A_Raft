using _00.Work.CheolYee._01.Codes.Projectiles;
using _00.Work.Resource.Manager;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Enemys.Attacks
{
    public class RangedAttack
    {
        private float _projectileSpeed;
        private readonly string _itemName;

        public RangedAttack(GameObject projectilePrefab, float projectileSpeed)
        {
            _projectileSpeed = projectileSpeed;
            _itemName = projectilePrefab.GetComponent<Projectile>().ItemName;
        }
        
        public void Attack(GroundRangeAttackEnemy enemy)
        {
            if (enemy.targetTrm == null) return;

            Vector2 dir = enemy.targetTrm.position - enemy.firePos.position;

            Projectile projectile = PoolManager.Instance.Pop(_itemName) as Projectile;
            if (projectile != null)
                projectile.Initialize(enemy.firePos, dir, enemy.enemyData.attackDamage, enemy.enemyData.knockbackPower, _projectileSpeed);

            Debug.Log($"{enemy.name} {enemy.firePos.name} 원거리 공격 발사!");
        }
        
        public void Attack(AirRangeAttackEnemy enemy)
        {
            if (enemy.targetTrm == null) return;

            Vector2 dir = enemy.targetTrm.position - enemy.firePos.position;

            Projectile projectile = PoolManager.Instance.Pop(_itemName) as Projectile;
            if (projectile != null)
                projectile.Initialize(enemy.firePos, dir, enemy.enemyData.attackDamage, enemy.enemyData.knockbackPower, _projectileSpeed);

            Debug.Log($"{enemy.name} {enemy.firePos.name} 원거리 공격 발사!");
        }

        public void Attack(Players.Player player)
        {
            if (player == null) return;
            
            Debug.Log($"{player.name} → 원거리 공격 발사!");
        }
    }
}