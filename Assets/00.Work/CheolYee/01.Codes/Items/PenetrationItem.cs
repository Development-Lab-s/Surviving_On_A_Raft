using _00.Work.CheolYee._01.Codes.Projectiles;
using _00.Work.Resource.Manager;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items
{
    public class PenetrationItem : ItemTypeProjectile
    {
        [Header("PenetrationItem Item")]
        public float detectionRadius;
        public ContactFilter2D enemyLayer;
        
        protected override void SpawnProjectile()
        {
            base.SpawnProjectile();

            Collider2D target = FindNearEnemy();
            if (target != null)
            {
                Projectile projectile = PoolManager.Instance.Pop(CurrentProjectileSo.itemName) as Projectile;
                Vector3 dir = target.transform.position - transform.position;
                if (projectile != null)
                    projectile.Initialize(transform, dir, Damage + Player.CurrentDamage, KnockbackPower, Speed);
            }
            else
            {
                LastSpawnTime += Cooldown;
            }
        }
        
        private void Update()
        {
            if (LastSpawnTime + Cooldown < Time.time)
            {
                SpawnProjectile();
            }
        }

        private Collider2D FindNearEnemy()
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius, enemyLayer.layerMask);
           
            if (hits.Length == 0) return null;
           
            Collider2D nearest = hits[0];
            float minDist = Vector2.Distance(transform.position, nearest.transform.position);

            foreach (var detect in hits)
            {
               float dist = Vector2.Distance(transform.position, detect.transform.position);
               if (dist < minDist)
               {
                   nearest = detect;
                   minDist = dist;
               }
            }
           
            return nearest;
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