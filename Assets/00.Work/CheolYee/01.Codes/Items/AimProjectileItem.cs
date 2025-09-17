using System;
using System.Collections;
using _00.Work.CheolYee._01.Codes.Projectiles;
using _00.Work.Resource.Manager;
using _00.Work.Resource.SO;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items
{
    public class AimProjectileItem : ItemTypeProjectile
    {
        public float bulletCount = 1;

        protected override void SpawnProjectile()
        {
            bulletCount = CurrentProjectileSo.bulletCount;
            base.SpawnProjectile();
            Projectile projectile = PoolManager.Instance.Pop(BullItemName) as Projectile;
            
            Vector2 dir = Player.PlayerInput.MousePosition - (Vector2)transform.position;
            if (projectile != null)
            {
                projectile.Initialize(transform, dir, Damage + Player.CurrentDamage, KnockbackPower, Speed);
            }
        }

        private void Update()
        {
            if (LastSpawnTime + Cooldown < Time.time)
            {
                StartCoroutine(MultiSpawnProjectile());
            }
        }

        private IEnumerator MultiSpawnProjectile()
        {
            for (int i = 0; i < bulletCount; i++)
            {
                
                SpawnProjectile();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}