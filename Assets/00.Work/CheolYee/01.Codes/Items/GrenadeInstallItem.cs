using System.Collections;
using _00.Work.CheolYee._01.Codes.Projectiles;
using _00.Work.Resource.Manager;
using UnityEngine;

namespace _00.Work.CheolYee._01.Codes.Items
{
    public class GrenadeInstallItem : ItemTypeInstall
    {
        protected override void SpawnProjectile()
        {
            base.SpawnProjectile();
            Projectile projectile = PoolManager.Instance.Pop(InstallItemSo.installItem.name) as Projectile;
            
            if (projectile != null)
                projectile.Initialize(transform, Vector2.up, Player.CurrentDamage + Damage, KnockbackPower, Speed);
            else
            {
                LastSpawnTime += Cooldown;
            }
        }
        
        private void Update()
        {
            if (LastSpawnTime + Cooldown < Time.time)
            {
                StartCoroutine(MultiSpawnGrenade());
            }
        }
        
        private IEnumerator MultiSpawnGrenade()
        {
            for (int i = 0; i < GrenadeCount; i++)
            {
                SpawnProjectile();
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}