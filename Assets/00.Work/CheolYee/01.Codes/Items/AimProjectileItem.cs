using System.Collections;
using _00.Work.CheolYee._01.Codes.Projectiles;
using _00.Work.CheolYee._01.Codes.SO;
using _00.Work.Resource.Manager;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items
{
    public class AimProjectileItem : ItemTypeProjectile
    {
        [SerializeField] private PlayerInputSo playerInput;
        public float bulletCount = 1;
        
        protected override void SpawnProjectile()
        {
            base.SpawnProjectile();
            Projectile projectile = PoolManager.Instance.Pop(CurrentProjectileSo.itemName) as Projectile;
            
            Vector2 dir = playerInput.MousePosition - (Vector2)transform.position;
            if (projectile != null)
                projectile.Initialize(transform, dir, Damage, KnockbackPower, Speed);
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