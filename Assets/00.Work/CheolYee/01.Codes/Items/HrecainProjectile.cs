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

            if (projectile != null)
            {
                Vector3 dir = firePos.right * Mathf.Sign(transform.localScale.x);

                projectile.Initialize(
                    firePos,   // 발사 위치 기준을 firePos로 넘겨주는 게 일반적
                    dir, 
                    Damage + Player.CurrentDamage, 
                    KnockbackPower, 
                    Speed
                );
            }

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