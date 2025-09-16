using _00.Work.CheolYee._01.Codes.Projectiles;
using _00.Work.Resource.Manager;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items
{
    public class HrecainProjectile : ItemTypeProjectile
    {
        [Header("HrecainProjectile Item")]
        public float detectionRadius;
        public ContactFilter2D enemyLayer;
        public Transform firePos;
        
        protected override void SpawnProjectile()
        {
            base.SpawnProjectile();

                Projectile projectile = PoolManager.Instance.Pop(CurrentProjectileSo.projectilePrefab.name) as Projectile;
                Vector3 dir = firePos.transform.right;
                if (projectile != null)
                    projectile.Initialize(transform, dir, Damage + Player.CurrentDamage, KnockbackPower, Speed);
                LastSpawnTime = Time.time;
        }
        
        private void Update()
        {
            if (LastSpawnTime + Cooldown < Time.time)
            {
                SpawnProjectile();
            }
        }
        
#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
        }
#endif
    }
}